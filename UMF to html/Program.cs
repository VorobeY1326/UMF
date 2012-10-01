using System;
using System.IO;
using Newtonsoft.Json.Linq;
using UMF.Tokens;

namespace UMF
{
    public class Program
    {
        public static void Main()
        {
            var monitor = new AcmMonitor(JObject.Parse(File.ReadAllText("firstsample.umf")));
            Console.WriteLine("--");
            const string smth = "<$problemName$>";
            int offset = 0;
            var ans = (new VariableToken(monitor, new ParsingContext(null, 0))).Parse(smth, ref offset);
            Console.WriteLine(ans);
        }
    }
}