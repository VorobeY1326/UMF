namespace UMF.Tokens
{
    public class AnythingExceptCharsToken : IToken
    {
        private readonly char[] exceptingChars;

        public AnythingExceptCharsToken(params char[] exceptingChars)
        {
            this.exceptingChars = exceptingChars;
        }

        public string Parse(string input, ref int offset)
        {
            if (offset >= input.Length)
                return null;
            return (new ManyToken(new AnySymbolExceptToken(exceptingChars))).Parse(input, ref offset);
        }
    }
}