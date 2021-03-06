﻿using System.IO;
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
            Assert.AreEqual("place", (string) monitor.MonitorJson["contestPlace"]);
            Assert.AreEqual(2012, (int) monitor.MonitorJson["contestYear"]);
            Assert.AreEqual(1, (int)monitor.MonitorJson["contestMonth"]);
            Assert.AreEqual(2, (int) monitor.MonitorJson["contestProblemsCount"]);
            Assert.AreEqual(2, monitor.ContestProblemsNames.Count);
            Assert.AreEqual("A", monitor.ContestProblemsNames[0]);
            Assert.AreEqual("B", monitor.ContestProblemsNames[1]);
            Assert.AreEqual("link", (string) monitor.MonitorJson["contestLink"]);
            Assert.AreEqual(1, monitor.ContestStanding.Count);
            Assert.AreEqual("first", monitor.ContestStanding[0].TeamName);
            Assert.AreEqual("school", (string) monitor.ContestStanding[0].TeamResultJson["teamSchool"]);
            Assert.AreEqual("S", (string) monitor.ContestStanding[0].TeamResultJson["teamSchoolShort"]);
            Assert.AreEqual(1000, (int) monitor.ContestStanding[0].TeamResultJson["teamTotalTime"]);
            Assert.AreEqual(2, monitor.ContestStanding[0].TeamPlayers.Count);
            Assert.AreEqual("player0", monitor.ContestStanding[0].TeamPlayers[0]);
            Assert.AreEqual("player1", monitor.ContestStanding[0].TeamPlayers[1]);
            Assert.AreEqual(1, monitor.ContestStanding[0].TeamSolving.Count);
            Assert.AreEqual(true, monitor.ContestStanding[0].TeamSolving[0].ProblemAccepted);
            Assert.AreEqual(1, monitor.ContestStanding[0].TeamSolving[0].ProblemNumber);
            Assert.AreEqual(2, (int) monitor.ContestStanding[0].TeamSolving[0].ProblemResultJson["problemAttempts"]);
            Assert.AreEqual("10:00", (string) monitor.ContestStanding[0].TeamSolving[0].ProblemResultJson["problemTime"]);
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