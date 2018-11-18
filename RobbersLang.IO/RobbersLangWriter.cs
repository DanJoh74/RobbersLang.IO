using System;
using System.IO;
using System.Text;

namespace RobbersLang.IO
{
    public class RobbersLangWriter : TextWriter
    {
        private readonly TextWriter _writer;
        private string _buffer;
        private int _previous = -1;

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
            if (_buffer != null)
            {
                WriteToUnderlyingWriter(char.IsUpper(value) || !char.IsLetter(value) ? _buffer.ToUpper() : _buffer, _buffer[0]);
                _buffer = null;
            }

            if (RobbersLang.SpecialCharacters.TryGetValue(value, out var encodedValue))
            {
                if (char.IsUpper(value))
                    _buffer = encodedValue;
                else
                    WriteToUnderlyingWriter(encodedValue, value);
            }
            else
            {
                WriteToUnderlyingWriter(value);
            }
        }

        public override void Close()
        {
            FlushBuffer();
            _writer.Close();
        }

        public override void Flush()
        {
            // Can't flush the buffer. Need to wait for next character in order
            // to determine whether or not the encoded value should be written
            // in all caps.

            _writer.Flush();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                FlushBuffer();
                _writer.Dispose();
            }
        }

        private void WriteToUnderlyingWriter(object value, char original = '\0')
        {
            _previous = value as char? ?? original;
            _writer.Write(value);
        }

        private void FlushBuffer()
        {
            if (_buffer == null) return;

            WriteToUnderlyingWriter(
                _previous != -1 && (char.IsUpper((char) _previous) || !char.IsLetter((char) _previous))
                    ? _buffer.ToUpper()
                    : _buffer, _buffer[0]);
            _previous = _buffer[0];
            _buffer = null;
        }
    }
}