using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace args
{
    class Program
    {
        static void Main(string[] args)
        {
            var schemaList = new List<SchemaItem>() {
                new SchemaItem { Name = "f", FlagType = typeof(String) },
                new SchemaItem { Name = "g", FlagType = typeof(Int32) },
                new SchemaItem { Name = "h", FlagType = typeof(Boolean) },
                new SchemaItem { Name = "i", FlagType = typeof(Single) } };

            var p = new Parser(args, schemaList);

            Console.WriteLine("-f: " + p.GetValues<string>("f"));

            Console.WriteLine("-g: " + p.GetValues<int>("g"));

            Console.WriteLine("-h: " + p.GetValues<bool>("h"));

            Console.WriteLine("-i: " + p.GetValues<float>("i"));

            Console.ReadKey();
        }
    }
}
