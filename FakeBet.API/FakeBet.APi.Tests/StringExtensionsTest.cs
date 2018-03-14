using System;
using System.Collections.Generic;
using System.Text;

namespace FakeBet.API.Tests
{
    using FakeBet.API.Extensions;

    using Xunit;

    public class StringExtensionsTest
    {
        [Fact]
        public void ShouldRemoveSpaces()
        {
            var a = "test test";
            a = a.RemoveAllSpaces();
            Assert.Equal("testtest", a);
        }

        [Fact]
        public void ShouldReturnNullWhenStringIsNull()
        {
            string a = null;
            Assert.Null(a.RemoveAllSpaces());
        }

        [Fact]
        public void ShouldReturnStringWithoutChangesWhenNoSpaces()
        {
            var a = "test";
            Assert.Equal("test", a.RemoveAllSpaces());
        }

        [Fact]
        public void ShouldReturnEmptyStringWhenEmptyStringGiven()
        {
            var a = "";
            Assert.Equal("",a.RemoveAllSpaces());
        }
    }
}