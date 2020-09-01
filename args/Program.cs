using System;
using System.Collections.Generic;

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

            try
            {
                var p = new Parser(args, schemaList);

                Console.WriteLine("-f: " + p.GetValues<string>("f"));

                Console.WriteLine("-g: " + p.GetValues<int>("g"));

                Console.WriteLine("-h: " + p.GetValues<bool>("h"));

                Console.WriteLine("-i: " + p.GetValues<float>("i"));
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(1);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(2);
            }
        }
    }
}
