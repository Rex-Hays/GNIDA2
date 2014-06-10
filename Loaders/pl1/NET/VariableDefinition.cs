using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GNIDA.Loaders
{
    public class VariableDefinition
    {
        public VariableDefinition(int index, TypeReference type)
        {
            Index = index;
            VariableType = type;
            Name = "";
        }

        public VariableDefinition(string name, int index, TypeReference type)
        {
            Index = index;
            VariableType = type;
            Name = name;
        }

        public int Index { get; internal set; }
        public TypeReference VariableType { get; internal set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return "[" + Index + "]" + (Name != null ? Name : "");
        }
    }
}
