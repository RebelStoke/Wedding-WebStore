using System;
using Xunit;

namespace UnitTesting
{
    public class AuthenticationTest
    {
        [Theory]
        [InlineData(true)]
        public void BaseLineTest(Boolean isTrue)
        {

            Assert.Equal(true, isTrue);
        }

        [Fact]
        public void BaseLineTest_ThrowsArgumentException()
        {

           // Assert.Throws<ArgumentException>(actual);
        }
    }
}
