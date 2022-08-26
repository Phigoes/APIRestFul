using API.Entities;

namespace API.Tests.Entities
{
    public class VideoTests
    {
        [Fact]
        public void Video_Validate_Title_Length()
        {
            //Arrange & Act
            var result = Assert.Throws<DomainException>(() => new Video(
                "Entretenimento",
                "A Band preparou uma série de atrações para agitar o fim de ano. Nesta terça-feira (21/12), às 22h30, o público acompanha o MasterChef Especial de Natal com a presença de vários famosos. Na primeira prova, Adriana Birolli e Toni Garrido enfrentam Negra Li e Felipe Titto. A dupla que fizer o melhor hambúrguer com acompanhamento e molho vence a disputa.",
                "Da Redação",
                "http://localhost:5036/imgs/",
                "http://localhost:5036/video/",
                API.Entities.Enums.Status.Active));

            //Assert
            Assert.Equal("Title must have until 90 characters", result.Message);
        }

        [Fact]
        public void Video_Validate_Hat_Length()
        {
            //Arrange & Act
            var result = Assert.Throws<DomainException>(() => new Video(
                "Fim de ano da Band traz programas especiais, filmes e shows exclusivos",
                "Fim de ano da Band traz programas especiais, filmes e shows exclusivos",
                "Da Redação",
                "http://localhost:5036/imgs/",
                "http://localhost:5036/video/",
                API.Entities.Enums.Status.Active));

            //Assert
            Assert.Equal("Hat must have until 40 characters", result.Message);
        }

        [Fact]
        public void Video_Validate_Title_Empty()
        {
            //Arrange & Act
            var result = Assert.Throws<DomainException>(() => new Video(
                "Entretenimento",
                string.Empty,
                "Da Redação",
                "http://localhost:5036/imgs/",
                "http://localhost:5036/video/",
                API.Entities.Enums.Status.Active));

            //Assert
            Assert.Equal("Title must not be empty", result.Message);
        }

        [Fact]
        public void Video_Validate_Hat_Empty()
        {
            //Arrange & Act
            var result = Assert.Throws<DomainException>(() => new Video(
                string.Empty,
                "Fim de ano da Band traz programas especiais, filmes e shows exclusivos",
                "Da Redação",
                "http://localhost:5036/imgs/",
                "http://localhost:5036/video/",
                API.Entities.Enums.Status.Active));

            //Assert
            Assert.Equal("Hat must not be empty", result.Message);
        }
    }
}
