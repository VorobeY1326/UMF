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

        public static Variable CreateVariable(JToken token)
        {
            switch (token.Type)
            {
                case JTokenType.String:
                    return new Variable((string) token);
                case JTokenType.Integer:
                    return new Variable((int) token);
                default:
                    return null;
            }
        }

        public object Value;
        public VariableType Type;
    }
}