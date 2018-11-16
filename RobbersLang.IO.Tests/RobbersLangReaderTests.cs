using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RobbersLang.IO.Tests
{
    [TestClass]
    public class RobbersLangReaderTests
    {
        [DataTestMethod]
        [DataRow("Jojagog totalolaror Rorövovarorsospoproråkoketot!", "Jag talar Rövarspråket!")]
        [DataRow("I'mom sospopeakokinongog Rorobobboberor'sos lolanongoguagoge!", "I'm speaking Robber's language!")]
        public void When_reading_then_it_should_decode_from_Rövarspråket(string input, string expected)
        {
            var subject = new RobbersLangReader(new StringReader(input));

            var actual = subject.ReadToEnd();

            actual.Should().Be(expected);
        }
    }
}