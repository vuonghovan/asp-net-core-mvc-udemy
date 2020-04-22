using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Utilities.Enums;

namespace Utilities
{

    public static class RegexHelper
    {
        /// <summary>
        /// Remove all non-numberic
        /// </summary>
        /// <param name="str"></param>
        /// <returns>Ex: 21.254.g000f => 21254000</returns>
        public static string ReplaceNonNumberic(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            return Regex.Replace(str, @$"{RegexDefines.NonNumberic}", string.Empty);
        }
    }
}
