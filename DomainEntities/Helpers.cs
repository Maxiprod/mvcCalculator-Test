using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities
{
    public static class Helpers
    {
        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        public static bool IsNumeric(this string text)
        {
            double value;

            return double.TryParse(text, out value);
        }

        public static void AddValue(this ArrayList list, string value)
        {

            if (list.Count == 0 ||
                list[list.Count - 1].ToString() != value)
            {
                list.Add(value);
            }
        }

    }
}
