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
            DecodingDictionary = RobbersLang.SpecialCharacters.ToDictionary(
                specialCharacter => specialCharacter.Encoded, specialCharacter => (int) specialCharacter.Character);
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
                return _buffer.Dequeue();

            // Found an encoded character. Clear the buffer and return the decoded value.
            _buffer.Clear();
            return decodedValue;
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