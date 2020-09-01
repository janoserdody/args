using System;
using System.Collections.Generic;
using System.Linq;

namespace args
{
    public class Parser
    {
        string[] args;
        IDictionary<string, object> argsDict;
        IList<SchemaItem> schemaList;

        public Parser(string[] args, IList<SchemaItem> schemaList)
        {
            this.args = args;

            this.schemaList = schemaList;

            this.argsDict = GetArgsDict(args);
        }
        
        public T GetValues<T>(string name)
        {
            if (string.IsNullOrEmpty(name))
                return default(T);

            var schema = schemaList.Where(x => x.Name == name).FirstOrDefault();

            if (schema == null)
            {
                throw new InvalidOperationException("Error: this flag is not exists: -" + name);
            }

            if (schema.FlagType != typeof(T))
            {
                throw new InvalidOperationException("Error: argument value is not correct type. Correct type is: " + schema.FlagType.Name);
            }

            T value = default(T);

            object returnValue;

            if (argsDict.TryGetValue(name, out returnValue))
            {
                value = (T)returnValue;
            }
            
            return value; 
        }

        private IDictionary<string, object> GetArgsDict(string[] args)
        {
            var keyValuePair = new KeyValuePair<string, object>();

            IDictionary<string, object> resultDict = new Dictionary<string, object>();

            object value = null;

            string name = string.Empty;

            for (int i = 0; i < args.Length; ++i)
            {
                value = null;

                if (IsInSchema(args[i]))
                {
                    name = args[i].Substring(1);
                    value = (object)GetDefaultFlagValue(name);

                    if (i + 1 < args.Length && IsValue(args[i + 1]))
                    {
                        value = GetValue(name, args[i + 1]);
                        i++;
                    }

                    keyValuePair = new KeyValuePair<string, object>(name, value);

                    if (!resultDict.ContainsKey(name))
                    {
                        resultDict.Add(keyValuePair);
                    }
                    else
                    {
                        throw new InvalidOperationException("Error: duplicated argument: -" + name);
                    }
                }
                else
                {
                    throw new InvalidOperationException("Error: the flag is not in the schema: " + args[i]);
                }
            }
            return resultDict;
        }

        private object GetDefaultFlagValue(string name)
        {
            object value = null;

            var type = schemaList.Where(flag => flag.Name == name).FirstOrDefault()?.FlagType;

            if (type == null)
            {
                return null;
            }

            var typeName = type.Name;

            switch (typeName)
            {
                case "Int32": value = 0;
                    break;
                case "String": value = string.Empty;
                    break;
                    // if there is a flag = true
                    // if there is no flag = false
                case "Boolean": value = true;
                    break;
                case "Single": value = 0.0f;
                    break;
            }

            return value;
        }

        private bool IsValue(string v)
        {
            var isValue = false;

            var number = 0;

            if (v.IndexOf("-") == 0)
            {
                if (v.Substring(1, 1) == "0" || int.TryParse(v.Substring(1), out number))
                {
                    isValue = true;
                }
            }
            else
            {
                isValue = true;
            }

            return isValue;
        }

        private bool IsInSchema(string arg)
        {
            var isInSchema = false;

            if (arg.IndexOf('-') == 0)
            {
                var name = arg.Substring(1);
                var flag = schemaList.Where(x => x.Name == name).FirstOrDefault();
                if (flag?.Name == name)
                {
                    isInSchema = true;
                }
            }

            return isInSchema;
        }

        private object GetValue(string name, string arg)
        {
            object value = null;

            var flag = schemaList.Where(x => x.Name == name).FirstOrDefault();

            if (flag == null)
            {
                return null;
            }

            var type = flag?.FlagType;

            var typeName = type.Name;

            switch (typeName)
            {
                case "Int32":
                    int number;
                    if (Int32.TryParse(arg, out number))
                    {
                        value = (object)number;
                    }
                    else
                    {
                        throw new ArgumentException("Invalid type of the argument. Argument name: " + flag.Name + ", invalid type: " + type.Name);
                    }
                    break;

                case "String":
                    value = arg;
                    break;

                case "Boolean":
                    value = true;
                    break;

                case "Single":
                    float number_f;
                    if (float.TryParse(arg, out number_f))
                    {
                        value = (object)number_f;
                    }
                    else
                    {
                        throw new ArgumentException("Invalid type of the argument. Argument name: " + flag.Name + ", invalid type: " + type.Name);
                    }
                    break;
            }

            return value;
        }
    }
}
