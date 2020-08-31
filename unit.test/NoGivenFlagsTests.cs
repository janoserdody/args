using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using args;
using Xunit;

namespace args.tests
{
    public class NoGivenFlagsTests
    {
        Parser GetParser(string[] param)
        {
            var schemaList = new List<SchemaItem>() {
                new SchemaItem { Name = "f", FlagType = typeof(String) },
                new SchemaItem { Name = "g", FlagType = typeof(Int32) }};

            var p = new Parser(param, schemaList);

            return p;
        }

        [Fact]
        public void IfNoFlagName_ReturnsNull()
        {
            // Arrange
            string[] param = { "-f", "akarmi", "-g", "11" };

            var p = GetParser(param);

            // Act
            var result = p.GetValues<string>("");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void noflag()
        {
            // Arrange
            string[] param = { "-f", "akarmi", "-z", "11" };

            var p = GetParser(param);

            // Act
            var result = p.GetValues<string>("");

            // Assert
            Assert.Null(result);
        }
    }
}
