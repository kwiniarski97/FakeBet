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
        public void Should_Remove_Spaces()
        {
            var a = "test test";
            a = a.RemoveAllSpaces();
            Assert.Equal("testtest", a);
        }

        [Fact]
        public void Should_Return_Null_When_String_Is_Null()
        {
            string a = null;
            Assert.Null(a.RemoveAllSpaces());
        }

        [Fact]
        public void Should_Return_String_Without_Changes_When_No_Spaces()
        {
            var a = "test";
            Assert.Equal("test", a.RemoveAllSpaces());
        }

        [Fact]
        public void Should_Return_Empty_String_When_Empty_String_Given()
        {
            var a = "";
            Assert.Equal("",a.RemoveAllSpaces());
        }
    }
}