using System.Globalization;
using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RobbersLang.IO.Tests
{
    [TestClass]
    public class RobbersLangWriterTests
    {
        [DataTestMethod]
        [DataRow("Jag talar Rövarspråket!", "Jojagog totalolaror Rorövovarorsospoproråkoketot!")]
        [DataRow("I'm speaking Robber's language!", "I'mom sospopeakokinongog Rorobobboberor'sos lolanongoguagoge!")]
        [DataRow(true, "Totrorue")]
        [DataRow('B', "Bob")]
        [DataRow("", "")]
        [DataRow("UPPER CASE", "UPOPPOPEROR COCASOSE")]
        [DataRow("Mixed UPPER lower", "Momixoxedod UPOPPOPEROR lolowoweror")]
        [DataRow("Toyota Material Handling AB", "Totoyotota Momatoterorialol Hohanondodlolinongog ABOB")]
        public void When_writing_then_it_should_encode_to_Rövarspråket(object input, string expected)
        {
            var stringWriter = new StringWriter();
            using (var subject = new RobbersLangWriter(stringWriter, CultureInfo.InvariantCulture))
            {
                subject.Write(input);
            }

            var actual = stringWriter.ToString();
            actual.Should().Be(expected);
        }
    }
}