using API.Entities;
using API.Infra.Interfaces;
using MongoDB.Driver;

namespace API.Infra
{
    public class MongoRepository<T> : IMongoRepository<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> _model;

        public MongoRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _model = database.GetCollection<T>(typeof(T).Name.ToLower());
        }

        public Result<T> Get(int page, int quantity)
        {
            var result = new Result<T>();
            result.Page = page;
            result.Quantity = quantity;

            var filter = Builders<T>.Filter.Eq(entity => entity.Deleted, false);

            result.Data = _model.Find(filter)
                .SortByDescending(entity => entity.PublishDate)
                .Skip((page - 1) * quantity)
                .Limit(quantity).ToList();

            result.Total = _model.CountDocuments(filter);
            result.TotalPages = result.Total / quantity;

            return result;
        }

        public T Get(string id) => _model.Find<T>(news => news.Id == id && !news.Deleted).FirstOrDefault();

        public T Create(T news)
        {
            _model.InsertOne(news);
            return news;
        }
        public void Update(string id, T news) => _model.ReplaceOne(news => news.Id == id, news);

        public void Remove(string id) 
        {
            var news = Get(id);
            news.Deleted = true;
            _model.ReplaceOne(news => news.Id == id, news);

        }

        public T GetBySlug(string slug) => _model.Find<T>(news => news.Slug == slug && !news.Deleted).FirstOrDefault();
    }
}
