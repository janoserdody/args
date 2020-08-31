using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using args;
using NUnit;

namespace args.tests
{
    public class NoGivenFlagsTests
    {
        public void NoFlag()
        {
            // Arrange
            string[] param = { "-f akarmi", "-g 11" };
            var p = new Parser(param);

            // Act
            var result = (int)p.GetValues<int>("-x");

            // Assert
            
        }
    }
}
