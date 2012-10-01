using System;
using System.Collections.Generic;
using System.Linq;

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
                result = new Variable(globalVariable);
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
                    result = new Variable(teamLevelVariable);
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
                        result = new Variable(ptLevelVariable);
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
                    } //TODO море
                };
    }
}