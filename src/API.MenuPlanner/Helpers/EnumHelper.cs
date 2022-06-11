namespace API.MenuPlanner.Helpers
{
    public static class EnumHelper
    {
        public static T Parse<T>(string? value)
            where T : struct
        {
            value = value?.ToLower();
            bool parseSucceed = Enum.TryParse<T>(value ?? "", out T enumValue);
            if (!parseSucceed)
                throw new Exception($"Could not convert {value} to {typeof(T)}.");

            return enumValue;
        }

    }
}
