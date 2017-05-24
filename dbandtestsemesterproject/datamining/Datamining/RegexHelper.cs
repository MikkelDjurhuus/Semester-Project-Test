using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Datamining
{
    public static class RegexHelper
    {
        public static string GetTitle(string input)
        {
            string pattern = "(?i)(?<=title:).*";
            Match match = Regex.Match(input, pattern);
            if (match.Success)
            {
                return match.Value.Trim();
            }
            else { return ""; }
        }
        public static string GetAuthor(string input)
        {
            string pattern = "(?i)(?<=author:).*";
            Match match = Regex.Match(input, pattern);
            if (match.Success)
            {
                return match.Value.Trim();
            }
            else { return ""; }
        }
        public static List<string> GetCities(string input)
        {
            string pattern = @"([A-Z][\w-]*(\s+[A-Z][\w-]*)+)";
            List<string> matches = new List<string>();
            foreach (Match m in Regex.Matches(input, pattern))
            {
                //IMPLEMENT DB CITY
                matches.Add(m.Value);
            }
            return matches;
        }
    }
}
