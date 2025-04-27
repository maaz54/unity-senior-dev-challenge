using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace QuestNarration.Extensions
{
    public static class StringExtension
    {

        /// <summary>
        /// Parses the first number found in a string and returns it as an integer.
        /// </summary>
        public static int ParseNumber(this string trigger)
        {
            Regex regex = new(@"(\d+)");
            Match match = regex.Match(trigger);

            if (match.Success)
            {
                return int.Parse(match.Value);
            }
            else
            {
                return 0;
            }
        }
    }
}
