namespace UMF.Tokens
{
    public class TemplateToken : IToken
    {
        private readonly AcmMonitor monitor;

        public TemplateToken(AcmMonitor monitor)
        {
            this.monitor = monitor;
        }

        public string Parse(string input, ref int offset)
        {
            return (new ManyToken(
                        new OrToken(new PlainTextToken(),
                                    new VariableToken(monitor, new ParsingContext(null, null)))
                                    )).Parse(input, ref offset);
        }
    }
}