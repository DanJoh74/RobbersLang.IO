using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RobbersLang.IO
{
    public class RobbersLangReader : TextReader
    {
        private static readonly IReadOnlyDictionary<string, int> DecodingDictionary;
        private readonly Queue<char> _buffer = new Queue<char>(3);
        private readonly TextReader _reader;

        static RobbersLangReader()
        {
            // Prepare a dictionary to be used for decoding of special characters.
            DecodingDictionary = RobbersLang.SpecialCharacters
                .ToLookup(
                    specialCharacter => specialCharacter.Value.ToLower(), specialCharacter => specialCharacter.Key)
                .ToDictionary(
                    lookup => lookup.Key, lookup => (int) char.ToLower(lookup.First()),
                    StringComparer.OrdinalIgnoreCase);
        }

        public RobbersLangReader(TextReader reader)
        {
            _reader = reader;
        }

        public override int Peek()
        {
            return _buffer.Count > 0 ? _buffer.Peek() : _reader.Peek();
        }

        public override int Read()
        {
            Buffer();

            // If the buffer is empty there is nothing more to read.
            if (_buffer.Count == 0) return -1;

            if (!DecodingDictionary.TryGetValue(new string(_buffer.ToArray()), out var decodedValue))
            {
                int value = _buffer.Dequeue();

                // If the first character in the buffer is a special character but the
                // characters in the buffer do not match the encoded version of the special
                // character, then the 'Rövarspråket' format is incorrect.
                if (RobbersLang.SpecialCharacters.ContainsKey((char) value))
                    throw new FormatException("Invalid 'Rövarspråket' format.");

                return value;
            }

            // Found an encoded character. Clear the buffer and return the decoded value.
            var upper = char.IsUpper(_buffer.Peek());
            _buffer.Clear();
            return upper ? char.ToUpper((char) decodedValue) : decodedValue;
        }

        public override void Close()
        {
            _reader.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _reader.Dispose();
        }

        private void Buffer()
        {
            int value;
            while (_buffer.Count < 3 && (value = _reader.Read()) >= 0)
                _buffer.Enqueue((char) value);
        }
    }
}