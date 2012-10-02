using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace UMF
{
    public class VariablesHolder
    {
        private readonly AcmMonitor monitor;
        private readonly ParsingContext context;

        public VariablesHolder(AcmMonitor monitor, ParsingContext context)
        {
            this.monitor = monitor;
            this.context = context;
        }

        public string GetStringVariable(string name)
        {
            var result = GetVariable(name);
            if (result != null && result.Type == VariableType.String)
                return (string) result.Value;
            return null;
        }

        public int? GetIntegerVariable(string name)
        {
            var result = GetVariable(name);
            if (result != null && result.Type == VariableType.Integer)
                return (int) result.Value;
            return null;
        }

        private Variable GetVariable(string name)
        {
            Variable result = null;
            Func<AcmMonitor, ParsingContext, Variable> additionalVariableGenerator = null;
            if (additionalVariables.ContainsKey(name))
                additionalVariableGenerator = additionalVariables[name];
            // global level
            var globalVariable = monitor.MonitorJson[name];
            if (globalVariable != null)
                result = Variable.CreateVariable(globalVariable);
            if (additionalVariableGenerator != null)
            {
                var additionalGlobalVariable = additionalVariableGenerator(monitor, new ParsingContext(null, null));
                if (additionalGlobalVariable != null)
                    result = additionalGlobalVariable;
            }
            // team level
            if (context.TeamNumber != null)
            {
                var teamLevelVariable = monitor.ContestStanding[context.TeamNumber.Value].TeamResultJson[name];
                if (teamLevelVariable != null)
                    result = Variable.CreateVariable(teamLevelVariable);
                if (additionalVariableGenerator != null)
                {
                    var additionalTeamLevelVariable = additionalVariableGenerator(monitor, new ParsingContext(context.TeamNumber, null));
                    if (additionalTeamLevelVariable != null)
                        result = additionalTeamLevelVariable;
                }
            }
            // problem level
            if (context.ProblemNumber != null)
            {
                if (additionalVariableGenerator != null)
                {
                    var additionalProblemLevelVariable = additionalVariableGenerator(monitor, new ParsingContext(null, context.ProblemNumber));
                    if (additionalProblemLevelVariable != null)
                        result = additionalProblemLevelVariable;
                }
            }
            // problem and team level
            if (context.TeamNumber != null && context.ProblemNumber != null)
            {
                var problemResult = monitor.ContestStanding[context.TeamNumber.Value].TeamSolving.FirstOrDefault(ts => ts.ProblemNumber == context.ProblemNumber);
                if (problemResult != null)
                {
                    var ptLevelVariable = problemResult.ProblemResultJson[name];
                    if (ptLevelVariable != null)
                        result = Variable.CreateVariable(ptLevelVariable);
                }
                if (additionalVariableGenerator != null)
                {
                    var additionalTeamLevelVariable = additionalVariableGenerator(monitor, new ParsingContext(context.TeamNumber, context.ProblemNumber));
                    if (additionalTeamLevelVariable != null)
                        result = additionalTeamLevelVariable;
                }
            }
            return result;
        }


        private readonly IDictionary<string, Func<AcmMonitor, ParsingContext, Variable>> additionalVariables =
            new Dictionary<string, Func<AcmMonitor, ParsingContext, Variable>>
                {
                    {
                        "problemName",
                        (monitor, context) =>
                            {
                                if (context.ProblemNumber == null ||
                                    monitor.ContestProblemsNames == null ||
                                    monitor.ContestProblemsNames.Count <= context.ProblemNumber)
                                    return null;
                                return new Variable(monitor.ContestProblemsNames[context.ProblemNumber.Value]);
                            }
                    },
                    {
                        "problemTeamsTried",
                            (monitor, context) =>
                            {
                                if (context.ProblemNumber == null || monitor.ContestStanding == null)
                                    return null;
                                int teamsTried = monitor.ContestStanding.Select(
                                    teamResult => teamResult.TeamSolving.FirstOrDefault(p => p.ProblemNumber == context.ProblemNumber)
                                    ).Count(problem => problem != null);
                                return new Variable(teamsTried);
                            }
                    },
                    {
                        "problemTeamsAccepted",
                        (monitor, context) =>
                            {
                                if (context.ProblemNumber == null || monitor.ContestStanding == null)
                                    return null;
                                int teamsAccepted = monitor.ContestStanding.Select(
                                    teamResult => teamResult.TeamSolving.FirstOrDefault(p => p.ProblemNumber == context.ProblemNumber)
                                    ).Count(problem => problem != null && problem.ProblemAccepted);
                                return new Variable(teamsAccepted);
                            }
                    },
                    {
                        "problemTotalAttempts",
                        (monitor, context) =>
                            {
                                if (context.ProblemNumber == null || monitor.ContestStanding == null)
                                    return null;
                                int totalAttempts = 0;
                                foreach (var teamResult in monitor.ContestStanding)
                                {
                                    var problem = teamResult.TeamSolving.FirstOrDefault(p => p.ProblemNumber == context.ProblemNumber);
                                    if (problem == null)
                                        continue;
                                    var attempts = problem.ProblemResultJson["problemAttempts"];
                                    if (attempts != null && attempts.Type == JTokenType.Integer)
                                        totalAttempts += (int) attempts;
                                }
                                return new Variable(totalAttempts);
                            }
                    },
                    {
                        "problemAcceptedAttempts",
                        (monitor, context) =>
                            {
                                if (context.ProblemNumber == null || monitor.ContestStanding == null)
                                    return null;
                                int attemptsAccepted = monitor.ContestStanding.Select(
                                    teamResult => teamResult.TeamSolving.FirstOrDefault(p => p.ProblemNumber == context.ProblemNumber)
                                    ).Count(problem => problem != null && problem.ProblemAccepted);
                                return new Variable(attemptsAccepted);
                            }
                    },
                    {
                        "teamPlayers",
                        (monitor, context) =>
                            {
                                if (context.TeamNumber == null || monitor.ContestStanding == null)
                                    return null;
                                var players = monitor.ContestStanding[context.TeamNumber.Value].TeamPlayers;
                                if (players == null)
                                    return null;
                                return new Variable(string.Join(", ", players));
                            }
                    },
                    {
                        "teamsCount",
                        (monitor, context) =>
                            {
                                return new Variable(monitor.ContestStanding.Count);
                            }
                    },
                    {
                        "problemsCount",
                        (monitor, context) =>
                            {
                                var userDefinedCount = monitor.MonitorJson["contestProblemsCount"];
                                if (userDefinedCount != null && userDefinedCount.Type == JTokenType.Integer)
                                    return new Variable((int) userDefinedCount);
                                var userDefinedNames = monitor.MonitorJson["contestProblemsNames"];
                                if (userDefinedNames != null && userDefinedNames.Type == JTokenType.Array)
                                    return new Variable(((JArray)userDefinedNames).Count);
                                int maximumOfProblemNumbers = -1;
                                foreach (var team in monitor.ContestStanding)
                                {
                                    foreach (var problemResult in team.TeamSolving)
                                    {
                                        if (problemResult.ProblemNumber > maximumOfProblemNumbers)
                                            maximumOfProblemNumbers = problemResult.ProblemNumber;
                                    }
                                }
                                return new Variable(maximumOfProblemNumbers + 1);
                            }
                    }
                };
    }
}