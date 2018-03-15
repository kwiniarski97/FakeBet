using System;

using FakeBet.API.Extensions;

using Xunit;

namespace FakeBet.APi.Tests
{
    public class GenericExtensionsTest
    {
        [Fact]
        public void Can_Compare_Same_Propeties()
        {
            var a = new HelperClass("val", "obj", "string", true, 21, 37);
            var b = new HelperClass("val", "obj", "string", true, 21, 37);
            Assert.True(a.ArePropertiesSame(b, null));
        }

        [Fact]
        public void Can_Compare_Object_With_Different_Value()
        {
            var a = new HelperClass("val", "obj", "string", true, 21, 37);
            var b = new HelperClass("notval", "notobj", "notstring", false, 31, 27);
            Assert.False(a.ArePropertiesSame(b, null));
        }

        [Fact]
        public void Will_Throw_Exception_When_One_Object_Is_Null()
        {
            var a = new HelperClass("val", "obj", "string", true, 21, 37);
            object b = null;
            Assert.Throws<Exception>(() => a.ArePropertiesSame(b, null));
        }

        [Fact]
        public void Derived_Types_Should_Return_False()
        {
            var a = new HelperClass("val", "obj", "string", true, 21, 37);
            var b = new HelperClass2("a");
            Assert.False(a.ArePropertiesSame(b, null));
        }

        [Fact]
        public void Can_Skip_Property()
        {
            var a = new HelperClass("difference", "obj", "string", true, 21, 37);
            var b = new HelperClass("different", "obj", "string", true, 21, 37);

            Assert.True(a.ArePropertiesSame(b, new[] { "dynamic" }));
        }

        private class HelperClass
        {
            public dynamic Dynamic { get; set; }

            public object Obj { get; set; }

            public string StringVar { get; set; }

            public bool BoolVar { get; set; }

            public short ShortVar { get; set; }

            public int IntVar { get; set; }

            public HelperClass(dynamic @dynamic, object obj, string stringVar, bool boolVar, short shortVar, int intVar)
            {
                this.Dynamic = dynamic;
                this.Obj = obj;
                this.StringVar = stringVar;
                this.BoolVar = boolVar;
                this.ShortVar = shortVar;
                this.IntVar = intVar;
            }

            public HelperClass()
            {
            }
        }

        private class HelperClass2 : HelperClass
        {
            public object A { get; set; }

            public HelperClass2(object a)
            {
                A = a;
            }
        }
    }
}