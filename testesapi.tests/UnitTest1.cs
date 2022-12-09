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
        public void ShouldTestResultWithParams(string expected1, string txt, int num)
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
            Assert.Equal(expected1, result.Txt);
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

        [Fact]
        public void ShouldThrowFirstException()
        {
            // Arrange
            var s = new ServiceService();

            var input = new DTOs.AInput
            {
                Num = -999,
                Txt = "huehuehue brbr"
            };

            // Act
            Action act = () => s.FuncaoA(input);

            // Assert
            Exception exception = Assert.Throws<Exception>(act);
            Assert.Equal("Deu Ruim", exception.Message);
        }

        [Theory]
        [InlineData("txttxt - 1", "eita desgraça", "txttxt", 1)]
        [InlineData(" - 222", "q isso??", "", 222)]
        [InlineData("aaa - 33", "ta errado", "aaa", 33)]
        public void ShouldNotReturnExpected2(string expected1, string expected2, string txt, int num)
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
            Assert.Equal(expected1, result.Txt);
            Assert.NotEqual(expected2, result.Txt);
        }
    }
}