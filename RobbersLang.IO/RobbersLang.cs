using System.Collections.Generic;
using System.Linq;

namespace RobbersLang.IO
{
    internal static class RobbersLang
    {
        // These are all the special characters in Rövarspråket, both upper and lower case.
        public static readonly IReadOnlyList<SpecialCharacter> SpecialCharacters =
            AllTheConsonantsAccordingToSwedes.SelectMany(c => new[] {char.ToUpper(c), char.ToLower(c)})
                .Select(c => new SpecialCharacter(c, new string(new[] {c, 'o', char.ToLower(c)})))
                .ToList();

        // ".. since Rövarspråket is a Swedish invention, your program should follow Swedish
        // rules regarding what is a vowel and what is a consonant."
        private const string AllTheConsonantsAccordingToSwedes = "BCDFGHJKLMNPQRSTVWXZ";

        internal class SpecialCharacter
        {
            public readonly char Character;
            public readonly string Encoded;

            public SpecialCharacter(char character, string encoded)
            {
                Character = character;
                Encoded = encoded;
            }
        }
    }
}