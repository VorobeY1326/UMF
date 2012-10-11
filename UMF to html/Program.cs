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
            string smth = File.ReadAllText("template.txt");
            int offset = 0;
            var ans = (new TemplateToken(monitor)).Parse(smth, ref offset);
            Console.WriteLine(ans);
        }
    }
}