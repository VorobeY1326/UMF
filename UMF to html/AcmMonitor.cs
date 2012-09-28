using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace UMF
{
    public class AcmMonitor
    {
        public readonly JObject MonitorJson;

        public AcmMonitor(JObject monitorJson)
        {
            MonitorJson = monitorJson;
            MonitorJson.ValidateRequiredFields("contestName", "contestStanding");
        }

        public string ContestName
        {
            get { return (string) MonitorJson["contestName"]; }
            set { MonitorJson["contestName"] = value; }
        }

        public string ContestPlace
        {
            get { return (string) MonitorJson["contestPlace"]; }
            set { MonitorJson["contestPlace"] = value; }
        }

        public int ContestYear
        {
            get { return (int) MonitorJson["contestYear"]; }
            set { MonitorJson["contestYear"] = value; }
        }

        public int ContestMonth
        {
            get { return (int) MonitorJson["contestMonth"]; }
            set { MonitorJson["contestMonth"] = value; }
        }

        public string ContestLink
        {
            get { return (string) MonitorJson["contestLink"]; }
            set { MonitorJson["contestLink"] = value; }
        }

        public int ContestProblemsCount
        {
            get { return (int) MonitorJson["contestProblemsCount"]; }
            set { MonitorJson["contestProblemsCount"] = value; }
        }

        public IList<string> ContestProblemsNames
        {
            get { return (MonitorJson["contestProblemsNames"]).Select(el => (string) el).ToList(); }
            set { MonitorJson["contestProblemsNames"] = new JArray(value); }
        }

        public IList<AcmTeamResult> ContestStanding
        {
            get { return (MonitorJson["contestStanding"]).Select(el => new AcmTeamResult((JObject)el)).ToList(); }
            set { MonitorJson["contestStanding"] = new JObject(value); }
        }
    }

    public class AcmTeamResult
    {
        public readonly JObject TeamResultJson;

        public AcmTeamResult(JObject teamResultJson)
        {
            TeamResultJson = teamResultJson;
            TeamResultJson.ValidateRequiredFields("teamName", "teamSolving");
        }

        public string TeamName
        {
            get { return (string) TeamResultJson["teamName"]; }
            set { TeamResultJson["teamName"] = value; }
        }

        public string TeamSchool
        {
            get { return (string) TeamResultJson["teamSchool"]; }
            set { TeamResultJson["teamSchool"] = value; }
        }

        public string TeamSchoolShort
        {
            get { return (string) TeamResultJson["teamSchoolShort"]; }
            set { TeamResultJson["teamSchoolShort"] = value; }
        }

        public int TeamTotalTime
        {
            get { return (int) TeamResultJson["teamTotalTime"]; }
            set { TeamResultJson["teamTotalTime"] = value; }
        }

        public IList<string> TeamPlayers
        {
            get { return (TeamResultJson["teamPlayers"]).Select(el => (string) el).ToList(); }
            set { TeamResultJson["teamPlayers"] = new JArray(value); }
        }

        public IList<AcmProblemResult> TeamSolving
        {
            get { return (TeamResultJson["teamSolving"]).Select(el => new AcmProblemResult((JObject)el)).ToList(); }
            set { TeamResultJson["teamSolving"] = new JObject(value); }
        }
    }

    public class AcmProblemResult
    {
        public readonly JObject ProblemResultJson;

        public AcmProblemResult(JObject problemResultJson)
        {
            ProblemResultJson = problemResultJson;
            ProblemResultJson.ValidateRequiredFields("problemNumber", "problemAccepted");
        }

        public int ProblemNumber
        {
            get { return (int)ProblemResultJson["problemNumber"]; }
            set { ProblemResultJson["problemNumber"] = value; }
        }

        public bool ProblemAccepted
        {
            get { return ( (int)ProblemResultJson["problemAccepted"] == 1 ); }
            set { ProblemResultJson["problemAccepted"] = value; }
        }

        public int ProblemAttempts
        {
            get { return (int)ProblemResultJson["problemAttempts"]; }
            set { ProblemResultJson["problemAttempts"] = value; }
        }

        public string ProblemTime
        {
            get { return (string)ProblemResultJson["problemTime"]; }
            set { ProblemResultJson["problemTime"] = value; }
        }
    }
}