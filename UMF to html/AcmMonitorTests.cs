using System.IO;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace UMF
{
    [TestFixture]
    public class AcmMonitorTests
    {
        [Test]
        public void MinimumParsing()
        {
            var monitor = new AcmMonitor(JObject.Parse(File.ReadAllText("../../testSamples/minimum.umf")));
            Assert.AreEqual("sample", monitor.ContestName);
            Assert.AreEqual(1, monitor.ContestStanding.Count);
            Assert.AreEqual("first", monitor.ContestStanding[0].TeamName);
            Assert.AreEqual(1, monitor.ContestStanding[0].TeamSolving.Count);
            Assert.AreEqual(true, monitor.ContestStanding[0].TeamSolving[0].ProblemAccepted);
            Assert.AreEqual(1, monitor.ContestStanding[0].TeamSolving[0].ProblemNumber);
        }

        [Test]
        public void FullParsing()
        {
            var monitor = new AcmMonitor(JObject.Parse(File.ReadAllText("../../testSamples/full.umf")));
            Assert.AreEqual("sample", monitor.ContestName);
            Assert.AreEqual("place", monitor.ContestPlace);
            Assert.AreEqual(2012, monitor.ContestYear);
            Assert.AreEqual(1, monitor.ContestMonth);
            Assert.AreEqual(2, monitor.ContestProblemsCount);
            Assert.AreEqual(2, monitor.ContestProblemsNames.Count);
            Assert.AreEqual("A", monitor.ContestProblemsNames[0]);
            Assert.AreEqual("B", monitor.ContestProblemsNames[1]);
            Assert.AreEqual("link", monitor.ContestLink);
            Assert.AreEqual(1, monitor.ContestStanding.Count);
            Assert.AreEqual("first", monitor.ContestStanding[0].TeamName);
            Assert.AreEqual("school", monitor.ContestStanding[0].TeamSchool);
            Assert.AreEqual("S", monitor.ContestStanding[0].TeamSchoolShort);
            Assert.AreEqual(1000, monitor.ContestStanding[0].TeamTotalTime);
            Assert.AreEqual(2, monitor.ContestStanding[0].TeamPlayers.Count);
            Assert.AreEqual("player0", monitor.ContestStanding[0].TeamPlayers[0]);
            Assert.AreEqual("player1", monitor.ContestStanding[0].TeamPlayers[1]);
            Assert.AreEqual(1, monitor.ContestStanding[0].TeamSolving.Count);
            Assert.AreEqual(true, monitor.ContestStanding[0].TeamSolving[0].ProblemAccepted);
            Assert.AreEqual(1, monitor.ContestStanding[0].TeamSolving[0].ProblemNumber);
            Assert.AreEqual(2, monitor.ContestStanding[0].TeamSolving[0].ProblemAttempts);
            Assert.AreEqual("10:00", monitor.ContestStanding[0].TeamSolving[0].ProblemTime);
        }

        [Test]
        public void ParsingWithAdditionalFields()
        {
            var monitor = new AcmMonitor(JObject.Parse(File.ReadAllText("../../testSamples/additional.umf")));
            Assert.AreEqual(JTokenType.String, monitor.MonitorJson["contestSecret"].Type);
            Assert.AreEqual("secret", (string) monitor.MonitorJson["contestSecret"]);
            Assert.AreEqual(1, monitor.ContestStanding.Count);
            Assert.AreEqual(JTokenType.String, monitor.ContestStanding[0].TeamResultJson["teamMystery"].Type);
            Assert.AreEqual("mystery", (string) monitor.ContestStanding[0].TeamResultJson["teamMystery"]);
            Assert.AreEqual(1, monitor.ContestStanding[0].TeamSolving.Count);
            Assert.AreEqual(JTokenType.Integer,
                            monitor.ContestStanding[0].TeamSolving[0].ProblemResultJson["problemInt"].Type);
            Assert.AreEqual(1326, (int) monitor.ContestStanding[0].TeamSolving[0].ProblemResultJson["problemInt"]);
        }

        [Test]
        public void ParsingWithoutRequiredFields()
        {
            try
            {
                var monitor = new AcmMonitor(JObject.Parse(File.ReadAllText("../../testSamples/error.umf")));
                Assert.AreEqual(1, monitor.ContestStanding[0].TeamSolving.Count);
                Assert.Fail("Mustn't parse JSON without 'problemAccepted' field");
            }
            catch (RequiredFieldNotFoundException)
            {
                Assert.Pass();
            }
        }
    }
}