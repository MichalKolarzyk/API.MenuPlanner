namespace API.MenuPlanner.Helpers
{
    public static class DateHelper
    {
        public static DateTime ToDateTime(string? date)
        {
            DateTime day = DateTime.Today;
            if (!string.IsNullOrEmpty(date))
            {
                bool dayParseResuly = DateTime.TryParse(date, out day);
                if (!dayParseResuly)
                    throw new Exception($"Day format is not correct, format example: dd-mm-yyyy");

                day = DateTime.SpecifyKind(day, DateTimeKind.Utc);
            }
            return day;
        }

    }
}
