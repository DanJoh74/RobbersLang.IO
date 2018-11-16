using System.Collections.Generic;
using System.Linq;

namespace RobbersLang.IO
{
    internal static class RobbersLang
    {
        // These are all the special characters in Rövarspråket, both upper and lower case.
        public static readonly IReadOnlyDictionary<char, string> SpecialCharacters =
            AllTheConsonantsAccordingToSwedes.SelectMany(c => new[] {char.ToUpper(c), char.ToLower(c)})
                .ToDictionary(c => c, c => new string(new[] {c, 'o', char.ToLower(c)}));

        // ".. since Rövarspråket is a Swedish invention, your program should follow Swedish
        // rules regarding what is a vowel and what is a consonant."
        private const string AllTheConsonantsAccordingToSwedes = "BCDFGHJKLMNPQRSTVWXZ";
    }
}