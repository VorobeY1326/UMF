namespace UMF.Tokens
{
    public class NumberToken : IToken
    {
        public string Parse(string input, ref int offset)
        {
            if (offset >= input.Length)
                return null;
            return (new AndToken(
                new OrToken(new ConstantToken("-"), new EmptyToken()),
                new DigitToken(),
                new ManyToken(new DigitToken())
                )).Parse(input, ref offset);
        }
    }
}