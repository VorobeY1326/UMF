using System.Text;

namespace UMF.Tokens
{
    public class AndToken : IToken
    {
        private readonly IToken[] tokens;

        public AndToken(params IToken[] tokens)
        {
            this.tokens = tokens;
        }

        public string Parse(string input, ref int offset)
        {
            int wasOffset = offset;
            var parcedResult = new StringBuilder();
            foreach (var token in tokens)
            {
                var res = token.Parse(input, ref offset);
                if (res == null)
                {
                    offset = wasOffset;
                    return null;
                }
                parcedResult.Append(res);
            }
            return parcedResult.ToString();
        }
    }
}