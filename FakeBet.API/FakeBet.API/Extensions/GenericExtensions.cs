using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FakeBet.API.Extensions
{
    public static class GenericExtensions
    {
        public static bool ArePropertiesSame<T>(this T self, T to, string[] ignoredProps)
        {
            ignoredProps = ignoredProps ?? new string[] { };

            if (self == null || to == null)
            {
                throw new Exception("One of the objects was null");
            }

            var selfType = self.GetType();
            var toType = to.GetType();

            if (selfType != toType)
            {
                return false;
            }

            var props = selfType.GetProperties();

            foreach (var prop in props)
            {
                if (ignoredProps.Contains(prop.Name))
                {
                    continue;
                }
                var selfValue = prop.GetValue(self);
                var toValue =  prop.GetValue(to);
                if (!selfValue.Equals(toValue))
                {
                    return false;
                }
            }

            return true;
        }
    }
}