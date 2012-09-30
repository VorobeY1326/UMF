namespace UMF.Tokens
{
    public class ConstantToken : IToken
    {
        private readonly string template;

        public ConstantToken(string template)
        {
            this.template = template;
        }

        public string Parse(string input, ref int offset)
        {
            if (offset + template.Length <= input.Length &&
                input.Substring(offset, template.Length) == template)
            {
                offset += template.Length;
                return input.Substring(offset - template.Length, template.Length);
            }
            return null;
        }
    }
}