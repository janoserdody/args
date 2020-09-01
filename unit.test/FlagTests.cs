using System;
using System.Collections.Generic;
using Xunit;
using args;

namespace unit.test
{
    public class FlagTests
    {
        Parser GetParser(string[] param)
        {
            var schemaList = new List<SchemaItem>() {
                new SchemaItem { Name = "f", FlagType = typeof(String) },
                new SchemaItem { Name = "g", FlagType = typeof(Int32) },
                new SchemaItem { Name = "h", FlagType = typeof(Boolean) },
                new SchemaItem { Name = "i", FlagType = typeof(Single) }};

        var p = new Parser(param, schemaList);

            return p;
        }

        [Fact]
        public void FlagInteger_Returns11()
        {
            // Arrange
            string[] param = { "-f", "akarmi", "-g", "11" };
            var p = GetParser(param);

            // Act
            var result = p.GetValues<int>("g");

            // Assert
            Assert.Equal(11, result);
        }

        [Fact]
        public void FlagInteger_ReturnsnegativeNumber()
        {
            // Arrange
            string[] param = { "-f", "sdfgsdf", "-g", "-22" };
            var p = GetParser(param);

            // Act
            var result = p.GetValues<int>("g");

            // Assert
            Assert.Equal(-22, result);
        }

        [Fact]
        public void FlagString_ReturnsAkarmi()
        {
            // Arrange
            string[] param = { "-f", "akarmi", "-g", "11" };
            var p = GetParser(param);

            // Act
            var result = p.GetValues<string>("f");

            // Assert
            Assert.Equal("akarmi", result);
        }

        [Fact]
        public void FlagBoolean_ReturnsTrue()
        {
            // Arrange
            string[] param = { "-f", "akarmi", "-g", "11", "-h"};
            var p = GetParser(param);

            // Act
            var result = p.GetValues<bool>("h");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GivenBooleanFlag_WhenNoFlagInArguments_ReturnsFalse()
        {
            // Arrange
            string[] param = { "-f", "akarmi", "-g", "11" };
            var p = GetParser(param);

            // Act
            var result = p.GetValues<bool>("h");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GivenDuplicatedFlagInArgs_ThanThrowsException()
        {
            // Arrange
            string[] param = { "-f", "akarmi", "-f", "fdsa" };

            // Assert
            Assert.Throws<InvalidOperationException>(() => GetParser(param));
        }

        [Fact]
        public void GivenInvalidFlag_ThanThrowsException()
        {
            // Arrange
            string[] param = { "-f", "akarmi", "-x", "fdsa" };

            // Assert
            Assert.Throws<InvalidOperationException>(() => GetParser(param));
        }

        [Fact]
        public void GivenInvalidArgumentTypeOfInt32_ThanThrowsException()
        {
            // Arrange
            string[] param = { "-f", "akarmi", "-g", "hgfdhgfd" };

            // Assert
            Assert.Throws<ArgumentException>(() => GetParser(param));
        }

        [Fact]
        public void GivenInvalidArgumentTypeOfFloat_ThanThrowsException()
        {
            // Arrange
            string[] param = { "-f", "akarmi", "-i", "sdfgsdfg" };

            // Assert
            Assert.Throws<ArgumentException>(() => GetParser(param));
        }

        [Fact]
        public void GivenValidArgumentTypeOfFloat_ThanReturnsTheExpectedValueRangeOfFloatNumber()
        {
            // Arrange
            string[] param = { "-f", "akarmi", "-i", "1.1" };
            var p = GetParser(param);

            // Act
            var result = p.GetValues<float>("i");

            // Assert
            Assert.InRange(result, 1.1, 1.2);
        }
    }
}
