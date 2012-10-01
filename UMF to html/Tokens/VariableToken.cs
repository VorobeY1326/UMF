namespace UMF.Tokens
{
    public class VariableToken : IToken
    {
        private readonly AcmMonitor monitor;
        private readonly ParsingContext context;

        public VariableToken(AcmMonitor monitor, ParsingContext context)
        {
            this.context = context;
            this.monitor = monitor;
        }

        public string Parse(string input, ref int offset)
        {
            if (offset >= input.Length)
                return null;
            if ((new ConstantToken("<$")).Parse(input, ref offset) == null)
                return null;
            var expression = (new AnythingExceptToken(new ConstantToken("$>")).Parse(input, ref offset));
            if ((new ConstantToken("$>")).Parse(input, ref offset) == null)
                throw new ParsingException("Can't find '$>' after '<$'");
            var variables = new VariablesHolder(monitor, context);
            var result = variables.GetStringVariable(expression);
            var intVar = variables.GetIntegerVariable(expression);
            if (intVar != null)
                result = string.Format("{0}", intVar.Value);
            return result;
        }
    }
}