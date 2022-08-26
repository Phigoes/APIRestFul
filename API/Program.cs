using API.Infra;
using API.Infra.Interfaces;
using API.Mappers;
using API.Services;
using API.Services.Interfaces;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
//using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearer" }
            },
            new string[] {}
        }
    });
});

#region [Database]
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
builder.Services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
#endregion

#region [Redis]
builder.Services.AddDistributedRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetSection("Redis:ConnectionString").Value;
});
#endregion

#region [HealthCheck]
builder.Services.AddHealthChecks()
    .AddMongoDb(builder.Configuration.GetSection("DatabaseSettings:ConnectionString").Value + "/" + builder.Configuration.GetSection("DatabaseSettings:db_portal").Value,
        name: "mongodb", tags: new string[] { "db", "data" })
    .AddRedis(builder.Configuration.GetSection("Redis:ConnectionString").Value, tags: new string[] { "db", "data" });

builder.Services.AddHealthChecksUI(options =>
{
    options.SetEvaluationTimeInSeconds(15);
    options.MaximumHistoryEntriesPerEndpoint(60);
    options.SetApiMaxActiveRequests(1);

    options.AddHealthCheckEndpoint("default api", "/health");
}).AddInMemoryStorage();
#endregion

#region [DI]
builder.Services.AddSingleton(typeof(IMongoRepository<>), typeof(MongoRepository<>));
builder.Services.AddSingleton<NewsService>();
builder.Services.AddSingleton<VideoService>();
builder.Services.AddSingleton<UploadService>();
builder.Services.AddSingleton<GalleryService>();

//builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
//builder.Services.AddSingleton<ICacheService, CacheMemoryService>();
builder.Services.AddSingleton<ICacheService, CacheRedisService>();
#endregion

#region [AutoMapper]
builder.Services.AddAutoMapper(typeof(EntityToViewModelMapping), typeof(EntityToViewModelMapping));
#endregion

#region [Cors]
builder.Services.AddCors();
#endregion

#region [JWT]
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                .GetBytes(builder.Configuration.GetSection("tokenManagement:secret").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API"));
}

#region [HealthCheck]
app.UseHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
}).UseHealthChecksUI(health => health.UIPath = "/health-ui");
#endregion

#region [Cors]
app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});
#endregion

#region [StaticFiles]
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Medias")),
    RequestPath = "/medias"
});
#endregion

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
