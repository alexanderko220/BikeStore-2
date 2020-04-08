using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace bikeStore.Data.Extensions
{
    public static class Extensions
    {
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes) where T : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }

            return query;
        }

        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }

        public static string SwapChar(this string value, char from, char to)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            StringBuilder builder = new StringBuilder();

            foreach (var item in value)
            {
                char c = item;
                if (c == from)
                    c = to;
                else if (c == to)
                    c = from;

                builder.Append(c);
            }
            return builder.ToString();
        }

        public static decimal ToDecimal(this string value)
        {
            decimal number;
            string tempValue = value;

            var punctuation = value.Where(x => char.IsPunctuation(x)).Distinct();
            int count = punctuation.Count();

            NumberFormatInfo format = CultureInfo.InvariantCulture.NumberFormat;
            switch (count)
            {
                case 0:
                    break;
                case 1:
                    var firstPunctuation = punctuation.ElementAt(0);
                    var firstPunctuationOccurence = value.Where(x => x == firstPunctuation).Count();

                    if (firstPunctuationOccurence == 1)
                    {
                        // we assume it's a decimal separator (and not a group separator)
                        value = value.Replace(firstPunctuation.ToString(), format.NumberDecimalSeparator);
                    }
                    else
                    {
                        // multiple occurence means that symbol is a group separator
                        value = value.Replace(firstPunctuation.ToString(), format.NumberGroupSeparator);
                    }

                    break;
                case 2:
                    if (punctuation.ElementAt(0) == '.')
                        tempValue = value.SwapChar('.', ',');
                    break;
                default:
                    throw new InvalidCastException();
            }

            number = decimal.Parse(tempValue, format);
            return number;
        }
    }

}
