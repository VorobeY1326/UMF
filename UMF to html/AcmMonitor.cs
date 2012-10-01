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

        public IList<string> ContestProblemsNames
        {
            get
            {
                if (MonitorJson["contestProblemsNames"] != null && MonitorJson["contestProblemsNames"].Type == JTokenType.Array)
                    return (MonitorJson["contestProblemsNames"]).Select(el => (string) el).ToList();
                return null;
            }
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

        public IList<string> TeamPlayers
        {
            get
            {
                if (TeamResultJson["teamPlayers"] != null && TeamResultJson["teamPlayers"].Type == JTokenType.Array)
                    return (TeamResultJson["teamPlayers"]).Select(el => (string) el).ToList();
                return null;
            }
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
    }
}