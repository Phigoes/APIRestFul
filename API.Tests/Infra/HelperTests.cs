using API.Infra;

namespace API.Tests.Infra
{
    public class HelperTests
    {
        [Fact]
        public void Should_Return_Validate_Slug()
        {
            //Arrange 
            var title = "Fim de ano da Band traz programas especiais, filmes e shows exclusivos";

            //Act
            var slug = Helper.GenerateSlug(title);

            //Assert
            Assert.Equal("fim-de-ano-da-band-traz-programas-especiais-filmes-e-shows-exclusivos", slug);
        }
    }
}
