using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CensusApp.Api.Extensions
{
    public static class PrimitiveTypeExtensions
    {
        #region int
        public static bool In(this int value, int[] itens)
        {
            return itens.Contains(value);
        }
        public static bool Between(this int value, int first, int last)
        {
            return value >= first && value <= last;
        }

        #endregion

        #region long
        public static bool In(this long value, long[] itens)
        {
            return itens.Contains(value);
        }
        public static bool Between(this long value, long first, long last)
        {
            return value >= first && value <= last;
        }
        #endregion  

        #region string
        public static string RemovePrep(this string str)
        {
            return str
                .Replace("de", string.Empty)
                .Replace("da", string.Empty)
                .Replace("das", string.Empty)
                .Replace("do", string.Empty)
                .Replace("dos", string.Empty)
                .Replace("di", string.Empty);

        }

        public static string RemoveAccents(this string str)
        {
            return Encoding.UTF8.GetString(System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(str));
        }
        public static bool IsDateTime(this string str)
        {
            DateTime dat;
            return DateTime.TryParse(str, out dat);
        }
        public static bool IsGuid(this string value)
        {
            Guid guid;
            return Guid.TryParse(value, out guid);
        }

        #endregion

        #region collections
        public static IEnumerable<T> SelectRecursive<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> selector)
        {
            foreach (var parent in source)
            {
                yield return parent;

                var children = selector(parent);
                foreach (var child in SelectRecursive(children, selector))
                    yield return child;
            }
        }
        #endregion


    }
}
