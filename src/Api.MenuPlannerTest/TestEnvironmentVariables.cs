using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.MenuPlannerTest
{
    public static class TestEnvironmentVariables
    {
        public enum Variable {
            MenuPlannerConnectionString,
        }
        public static string GetEnvironmentVariableValue(Variable variable)
        {
            var value = Environment.GetEnvironmentVariable(variable.ToString());
            if (value == null)
                value = _defaultVariables[variable];

            return value;
        }


        readonly static Dictionary<Variable, string> _defaultVariables = new Dictionary<Variable, string>()
        {
            {Variable.MenuPlannerConnectionString, "mongodb://localhost:27019" },
        };

    }
}
