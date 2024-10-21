using Microsoft.VisualStudio.TestTools.UnitTesting;
using Goke.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Goke.Core.Tests
{
    [TestClass()]
    public class TextTests
    {
        [TestMethod()]
        public void Generate_Generate_Text()
        {
            // Arrange
            var text = Text.Generate();

            // Act
            int specialLength = text.Count(c => Text.SPECIAL.Any(a => a == c));
            int upperAlphabethLength = text.Count(c => Text.UPPERALPHABETH.Any(a => a == c));
            int lowerAlphabethLength = text.Count(c => Text.LOWERALPHABETH.Any(a => a == c));
            int numberLength = text.Count(c => Text.NUMBER.Any(a => a == c));

            // Assert
            Assert.IsNotNull(text);
            Assert.IsTrue(text.Length == 16);
            Assert.IsTrue(specialLength == 4);
            Assert.IsTrue(upperAlphabethLength == 4);
            Assert.IsTrue(lowerAlphabethLength == 4);
            Assert.IsTrue(numberLength == 4);
        }

        [TestMethod()]
        public void GeneratePassword_Length21_Text()
        {
            // Arrange
            int length = 21;
            var text = Text.GeneratePassword(length);

            int textLength = length / 4;
            int reminderLength = length % 4;

            // Act
            int specialLength = text.Count(c => Text.SPECIAL.Any(a => a == c));
            int upperAlphabethLength = text.Count(c => Text.UPPERALPHABETH.Any(a => a == c));
            int lowerAlphabethLength = text.Count(c => Text.LOWERALPHABETH.Any(a => a == c));
            int numberLength = text.Count(c => Text.NUMBER.Any(a => a == c));

            // Assert
            Assert.IsNotNull(text);
            Assert.IsTrue(text.Length == length);
            Assert.IsTrue(specialLength == textLength);
            Assert.IsTrue(upperAlphabethLength == textLength);
            Assert.IsTrue(lowerAlphabethLength == (textLength+reminderLength));
            Assert.IsTrue(numberLength == textLength);
        }
    }
}