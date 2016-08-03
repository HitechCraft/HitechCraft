namespace HitechCraft.DAL.Domain.Extentions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class IEnumerableExtentions
    {
        /// <summary>
        /// Returns the last of [count] elements
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="list">IEnumerable</param>
        /// <param name="count">Count of elements</param>
        /// <returns></returns>
        public static IEnumerable<T> Limit<T>(this IEnumerable<T> list, int count)
        {
            var limitedList = list.Take(Math.Max(list.Count(), list.Count() - count));

            return limitedList;
        }
    }
}
