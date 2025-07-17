using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LC.CORE.Extensions
{
    public static class StringExtensions
    {
        public static string ToPascalCaseFormatWithSpaces(this string str)
        {
            string sample = string.Join("", str?.Select(c => Char.IsLetterOrDigit(c) ? c.ToString().ToLower() : "_").ToArray());

            var arr = sample?
                .Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => $"{s.Substring(0, 1).ToUpper()}{s.Substring(1)}");

            sample = string.Join(" ", arr);

            return sample;
        }
    }
}
