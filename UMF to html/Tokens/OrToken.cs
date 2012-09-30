namespace UMF.Tokens
{
    public class OrToken : IToken
    {
        private readonly IToken[] tokens;

        public OrToken(params IToken[] tokens)
        {
            this.tokens = tokens;
        }

        public string Parse(string input, ref int offset)
        {
            foreach (var token in tokens)
            {
                var res = token.Parse(input, ref offset);
                if (res != null)
                {
                    return res;
                }
            }
            return null;
        }
    }
}