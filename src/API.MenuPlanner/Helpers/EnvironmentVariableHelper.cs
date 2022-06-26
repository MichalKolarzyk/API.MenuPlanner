namespace API.MenuPlanner.Helpers
{
    public static class EnvironmentVariableHelper
    {
        public enum EnvironmentVariable
        {
            MenuPlannerConnectionString,
        }
        public static string? TryGetValue(EnvironmentVariable variable)
        {
            return Environment.GetEnvironmentVariable(variable.ToString());
        }


        readonly static Dictionary<EnvironmentVariable, string> _defaultValues = new Dictionary<EnvironmentVariable, string>()
        {
            {EnvironmentVariable.MenuPlannerConnectionString, "mongodb://localhost:27017" },
        };
    }
}
