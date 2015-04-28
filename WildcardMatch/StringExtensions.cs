﻿// A very simple wildcard match
// https://github.com/picrap/WildcardMatch

namespace WildcardMatch
{
    using System.Linq;

    public static class StringExtensions
    {
        /// <summary>
        /// Tells if the given string matches the given wildcard.
        /// Two wildcards are allowed: '*' and '?'
        /// '*' matches 0 or more characters
        /// '?' matches any character
        /// </summary>
        /// <param name="wildcard">The wildcard.</param>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static bool WildcardMatch(this string wildcard, string s)
        {
            return WildcardMatch(wildcard, s, 0, 0);
        }

        /// <summary>
        /// Internal matching algorithm.
        /// </summary>
        /// <param name="wildcard">The wildcard.</param>
        /// <param name="s">The s.</param>
        /// <param name="wildcardIndex">Index of the wildcard.</param>
        /// <param name="sIndex">Index of the s.</param>
        /// <returns></returns>
        private static bool WildcardMatch(this string wildcard, string s, int wildcardIndex, int sIndex)
        {
            for (; ; )
            {
                // in the wildcard end, if we are at tested string end, then strings match
                if (wildcardIndex == wildcard.Length)
                    return sIndex == s.Length;

                var c = wildcard[wildcardIndex];
                switch (c)
                {
                    // always a match
                    case '?':
                        break;
                    case '*':
                        // if this is the last wildcard char, then we have a match, whatever the tested string is
                        if (wildcardIndex == wildcard.Length - 1)
                            return true;
                        // test if a match follows
                        return Enumerable.Range(sIndex, s.Length - 1).Any(i => WildcardMatch(wildcard, s, wildcardIndex + 1, i));
                    default:
                        if (c != s[sIndex])
                            return false;
                        break;
                }

                wildcardIndex++;
                sIndex++;
            }
        }
    }
}
