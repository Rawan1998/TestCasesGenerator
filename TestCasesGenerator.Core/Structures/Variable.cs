using System;

namespace TestCasesGenerator.Core.Structures
{
    public class Variable
    {
        public String Type;
        public String Name;
        public object Value;
        public bool IsInput;

        public Variable(String type, String name, String value, bool isInput)
        {
            this.Type = type;
            this.Name = name;
            this.IsInput = isInput;

            if (type == "int")
            {
                this.Value = Convert.ToInt32(value);   
            }
            else if (type == "float" || type == "double")
            {
                this.Value = Convert.ToDouble(value);
            }
            else if (type == "char")
            {
                this.Value = value[0];
            }
        }

        public Variable(String type, String name, bool isInput)
        {
            this.Type = type;
            this.Name = name;
            this.Value = null;
            this.IsInput = isInput;
        }
    }
}
