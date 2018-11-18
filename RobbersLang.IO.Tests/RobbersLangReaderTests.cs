using System;
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
        [DataRow("", "")]
        [DataRow("UPOPPOPEROR COCASOSE", "UPPER CASE")]
        [DataRow("Momixoxedod UPOPPOPEROR lolowoweror", "Mixed UPPER lower")]
        [DataRow("Totoyotota Momatoterorialol Hohanondodlolinongog ABOB", "Toyota Material Handling AB")]
        public void When_reading_then_it_should_decode_from_Rövarspråket(string input, string expected)
        {
            var subject = new RobbersLangReader(new StringReader(input));

            var actual = subject.ReadToEnd();

            actual.Should().Be(expected);
        }

        [DataTestMethod]
        [DataRow("Jag talar Rövarspråket!")]
        [DataRow("T")]
        public void When_reading_invalid_content_then_it_should_throw_FormatException(string input)
        {
            var subject = new RobbersLangReader(new StringReader(input));

            Action act = () => subject.ReadToEnd();

            act.Should().Throw<FormatException>();
        }

        [DataTestMethod]
        [DataRow("abobcoc", 2, 'c')]
        [DataRow("ÅÄÖ", 1, 'Ä')]
        [DataRow("bob", 1, -1)]
        public void When_peeking_then_it_should_return_the_next_character(
            string input, int numberOfCharactersToReadBeforePeeking, int expected)
        {
            var subject = new RobbersLangReader(new StringReader(input));
            subject.Read(new Span<char>(new char[numberOfCharactersToReadBeforePeeking]));

            var actual = subject.Peek();

            actual.Should().Be(expected);
        }
    }
}