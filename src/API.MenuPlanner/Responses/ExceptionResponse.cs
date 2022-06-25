namespace API.MenuPlanner.Responses
{
    public class ExceptionResponse
    {
        public class BadRequestException : Exception
        {
            public BadRequestException(string message) : base(message)
            {
            }
        }

        public class ForbidException : Exception
        {
            public ForbidException(string message) : base(message)
            {
            }
        }

        public class NotFoundException : Exception
        {
            public NotFoundException(string message) : base(message)
            {
            }
        }


        public class Data
        {
            public int StatusCode { get; set; }
            public LogLevel LogLevel { get; set; }
        }
    }
}
