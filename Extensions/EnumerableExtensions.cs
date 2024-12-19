﻿namespace hijazi.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return (list is not null) && list.Any();
        }
        public static bool IsNotNullOrEmpty<T>(this ICollection<T> list)
        {
            return (list is not null) && list.Any();
        }
    }
}
