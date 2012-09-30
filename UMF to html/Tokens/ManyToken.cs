using System.Text;

namespace UMF.Tokens
{
    public class ManyToken : IToken
    {
        private readonly IToken token;

        public ManyToken(IToken token)
        {
            this.token = token;
        }

        public string Parse(string input, ref int offset)
        {
            var parcedResult = new StringBuilder();
            while (true)
            {
                var res = token.Parse(input, ref offset);
                if (res == null)
                    break;
                parcedResult.Append(res);
            }
            return parcedResult.ToString();
        }
    }
}