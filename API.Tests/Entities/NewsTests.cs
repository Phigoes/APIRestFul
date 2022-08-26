using API.Entities;

namespace API.Tests.Entities
{
    public class NewsTests
    {
        [Fact]
        public void News_Validate_Title_Length()
        {
            //Arrange & Act
            var result = Assert.Throws<DomainException>(() => new News(
                "Entretenimento",
                "A Band preparou uma série de atrações para agitar o fim de ano. Nesta terça-feira (21/12), às 22h30, o público acompanha o MasterChef Especial de Natal com a presença de vários famosos. Na primeira prova, Adriana Birolli e Toni Garrido enfrentam Negra Li e Felipe Titto. A dupla que fizer o melhor hambúrguer com acompanhamento e molho vence a disputa.",
                "A Band preparou uma série de atrações para agitar o fim de ano. Nesta terça-feira (21/12), às 22h30, o público acompanha o MasterChef Especial de Natal com a presença de vários famosos. Na primeira prova, Adriana Birolli e Toni Garrido enfrentam Negra Li e Felipe Titto. A dupla que fizer o melhor hambúrguer com acompanhamento e molho vence a disputa.",
                "Da Redação",
                "http://localhost:5056/imgs/",
                API.Entities.Enums.Status.Active));

            //Assert
            Assert.Equal("Title must have until 90 characters", result.Message);
        }

        [Fact]
        public void News_Validate_Hat_Length()
        {
            //Arrange & Act
            var result = Assert.Throws<DomainException>(() => new News(
                "Fim de ano da Band traz programas especiais, filmes e shows exclusivos",
                "Fim de ano da Band traz programas especiais, filmes e shows exclusivos",
                "A Band preparou uma série de atrações para agitar o fim de ano. Nesta terça-feira (21/12), às 22h30, o público acompanha o MasterChef Especial de Natal com a presença de vários famosos. Na primeira prova, Adriana Birolli e Toni Garrido enfrentam Negra Li e Felipe Titto. A dupla que fizer o melhor hambúrguer com acompanhamento e molho vence a disputa.",
                "Da Redação",
                "http://localhost:5056/imgs/",
                API.Entities.Enums.Status.Active));

            //Assert
            Assert.Equal("Hat must have until 40 characters", result.Message);
        }

        [Fact]
        public void News_Validate_Title_Empty()
        {
            //Arrange & Act
            var result = Assert.Throws<DomainException>(() => new News(
                "Entretenimento",
                string.Empty,
                "A Band preparou uma série de atrações para agitar o fim de ano. Nesta terça-feira (21/12), às 22h30, o público acompanha o MasterChef Especial de Natal com a presença de vários famosos. Na primeira prova, Adriana Birolli e Toni Garrido enfrentam Negra Li e Felipe Titto. A dupla que fizer o melhor hambúrguer com acompanhamento e molho vence a disputa.",
                "Da Redação",
                "http://localhost:5056/imgs/",
                API.Entities.Enums.Status.Active));

            //Assert
            Assert.Equal("Title must not be empty", result.Message);
        }

        [Fact]
        public void News_Validate_Hat_Empty()
        {
            //Arrange & Act
            var result = Assert.Throws<DomainException>(() => new News(
                string.Empty,
                "Fim de ano da Band traz programas especiais, filmes e shows exclusivos",
                "A Band preparou uma série de atrações para agitar o fim de ano. Nesta terça-feira (21/12), às 22h30, o público acompanha o MasterChef Especial de Natal com a presença de vários famosos. Na primeira prova, Adriana Birolli e Toni Garrido enfrentam Negra Li e Felipe Titto. A dupla que fizer o melhor hambúrguer com acompanhamento e molho vence a disputa.",
                "Da Redação",
                "http://localhost:5056/imgs/",
                API.Entities.Enums.Status.Active));

            //Assert
            Assert.Equal("Hat must not be empty", result.Message);
        }

        [Fact]
        public void News_Validate_Text_Empty()
        {
            //Arrange & Act
            var result = Assert.Throws<DomainException>(() => new News(
                "Entretenimento",
                "Fim de ano da Band traz programas especiais, filmes e shows exclusivos",
                string.Empty,
                "Da Redação",
                "http://localhost:5056/imgs/",
                API.Entities.Enums.Status.Active));

            //Assert
            Assert.Equal("Text must not be empty", result.Message);
        }
    }
}
