namespace UMF.Tokens
{
    public class PlainTextToken : IToken
    {
        public string Parse(string input, ref int offset)
        {
            if (offset >= input.Length)
                return null;
            return
                new AnythingExceptToken(
                    new OrToken(
                        new ConstantToken("<@"),
                        new ConstantToken("<#"),
                        new ConstantToken("<$"))).Parse(input, ref offset);
        }
    }
}