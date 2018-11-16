using System;
using System.IO;
using System.Text;

namespace RobbersLang.IO
{
    public class RobbersLangWriter : TextWriter
    {
        private readonly TextWriter _writer;

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
            if (RobbersLang.SpecialCharacters.TryGetValue(value, out var encodedValue))
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