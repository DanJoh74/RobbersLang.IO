using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RobbersLang.IO
{
    public class RobbersLangWriter : TextWriter
    {
        private static readonly IReadOnlyDictionary<char, string> EncodingDictionary;
        private readonly TextWriter _writer;

        static RobbersLangWriter()
        {
            // Prepare a dictionary for optimal write performance.
            EncodingDictionary = RobbersLang.SpecialCharacters.ToDictionary(
                specialCharacter => specialCharacter.Character, specialCharacter => specialCharacter.Encoded);
        }

        public RobbersLangWriter(TextWriter writer)
            : this(writer, null)
        {
        }

        public RobbersLangWriter(TextWriter writer, IFormatProvider formatProvider)
            : base(formatProvider)
        {
            _writer = writer;
        }

        public override Encoding Encoding => _writer.Encoding;

        public override void Write(char value)
        {
            if (EncodingDictionary.TryGetValue(value, out var encodedValue))
                _writer.Write(encodedValue);
            else
                _writer.Write(value);
        }

        public override void Close()
        {
            _writer.Close();
        }

        public override void Flush()
        {
            _writer.Flush();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _writer.Dispose();
        }
    }
}