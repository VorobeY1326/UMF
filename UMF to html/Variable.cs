using System;
using Newtonsoft.Json.Linq;

namespace UMF
{
    public enum VariableType
    {
        String,
        Integer
    }

    public class Variable
    {
        public Variable(object value, VariableType type)
        {
            Value = value;
            Type = type;
        }

        public Variable(string s)
        {
            Value = s;
            Type = VariableType.String;
        }

        public Variable(int i)
        {
            Value = i;
            Type = VariableType.Integer;
        }

        public Variable(JToken token)
        {
            switch (token.Type)
            {
                case JTokenType.String:
                    Value = (string) token;
                    Type = VariableType.String;
                    break;
                case JTokenType.Integer:
                    Value = (int) token;
                    Type = VariableType.Integer;
                    break;
                default:
                    throw new Exception(String.Format("JTokenType {0} isn't convertable to Variable", token.Type));
            }
        }

        public object Value;
        public VariableType Type;
    }
}