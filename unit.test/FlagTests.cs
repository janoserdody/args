using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                new SchemaItem { Name = "i", FlagType = typeof(Int32) }};

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
            string[] param = { "-i", "-22", "-g", "11" };
            var p = GetParser(param);

            // Act
            var result = p.GetValues<int>("i");

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
    }
}
