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
            Console.WriteLine(monitor.ContestName);
            Console.WriteLine(monitor.ContestPlace);
            Console.WriteLine(monitor.ContestLink);
            Console.WriteLine(monitor.ContestYear);
            Console.WriteLine(monitor.ContestMonth);
            Console.WriteLine(monitor.ContestProblemsCount);
            foreach (var name in monitor.ContestProblemsNames)
            {
                Console.WriteLine(name);
            }
            foreach (var team in monitor.ContestStanding)
            {
                Console.WriteLine(team.TeamName);
                Console.WriteLine(team.TeamSchool);
                Console.WriteLine(team.TeamSchoolShort);
                foreach (var player in team.TeamPlayers)
                {
                    Console.WriteLine(player);
                }
                Console.WriteLine(team.TeamTotalTime);
                foreach (var res in team.TeamSolving)
                {
                    Console.WriteLine("<");
                    Console.WriteLine(res.ProblemNumber);
                    Console.WriteLine(res.ProblemAccepted);
                    Console.WriteLine(res.ProblemAttempts);
                    Console.WriteLine(res.ProblemTime);
                    Console.WriteLine(">");
                }
            }
            Console.WriteLine("--");
            const string smth = "Vary good! Very good? Oh, of course..";
            int offset = 0;
            var ans = (new StringWithoutCharsToken()).Parse(smth, ref offset);
            Console.WriteLine(ans);
        }
    }
}