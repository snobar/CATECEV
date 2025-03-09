using System.Reflection;

namespace CATECEV.CORE.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNotNullOrEmpty<T>(this T? value) where T : struct
        {
            return value.HasValue;
        }

        public static bool IsNotNullOrEmpty<T>(this T data)
        {
            if (data is null)
            {
                return false;
            }

            return true;
        }

        public static bool IsNotNullOrEmpty(this object obj)
        {
            if (obj is null)
                return false;

            return obj.GetType().GetRuntimeProperties().Any(property =>
            {
                object value = property.GetValue(obj);
                return (value is not null);
            });
        }

        public static T ToAnyType<T>(this object value)
        {
            var Type = typeof(T);
            var UnderlyingType = Type.UnderlyingSystemType;

            if (value == null)
            {
                return default;
            }
            if (UnderlyingType.IsNotNullOrEmpty())
            {
                return (T)Convert.ChangeType(value, UnderlyingType);
            }
            return (T)Convert.ChangeType(value, Type);
        }
    }
}
