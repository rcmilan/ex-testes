using testesapi.Exceptions;
using testesapi.Services;

namespace testesapi.tests
{
    public class UnitTest1
    {
        [Fact]
        public void ShouldTestResultNoParams()
        {
            // Arrange
            string expected = "a - 1";
            string txt = "a";
            int num = 1;

            var s = new ServiceService();

            var input = new DTOs.AInput
            {
                Txt = txt,
                Num = num
            };

            // Act
            var result = s.FuncaoA(input);

            // Assert
            Assert.Equal(expected, result.Txt);
        }

        [Theory]
        [InlineData("txttxt - 1", "txttxt", 1)]
        [InlineData(" - 222", "", 222)]
        [InlineData("aaa - 33", "aaa", 33)]
        public void ShouldTestResultWithParams(string expected, string txt, int num)
        {
            // Arrange
            var s = new ServiceService();

            var input = new DTOs.AInput
            {
                Txt = txt,
                Num = num
            };

            // Act
            var result = s.FuncaoA(input);

            // Assert
            Assert.Equal(expected, result.Txt);
        }

        [Fact]
        public void ShouldThrowException()
        {
            // Arrange
            var s = new ServiceService();

            var input = new DTOs.AInput
            {
                Num = -1,
                Txt = ""
            };

            // Act
            Action act = () => s.FuncaoA(input);

            // Assert
            Exception exception = Assert.Throws<Exception>(act);
            Assert.Equal("Deu Ruim", exception.Message);
        }

        [Fact]
        public void ShouldThrowCustomException()
        {
            // Arrange
            var s = new ServiceService();

            var input = new DTOs.AInput
            {
                Num = 999,
                Txt = "huehuehue brbr"
            };

            // Act
            Action act = () => s.FuncaoA(input);

            // Assert
            Exception exception = Assert.Throws<TextoMalucoException>(act);
            Assert.Equal("hu3", exception.Message);
        }
    }
}