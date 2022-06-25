using API.MenuPlanner.Responses;

namespace API.MenuPlanner.Services
{
    public class ErrorService
    {
        private readonly Dictionary<Type, ExceptionResponse.Data> _dataExceptionMapping = new Dictionary<Type, ExceptionResponse.Data>()
        {
            { typeof(ExceptionResponse.ForbidException),
                new ExceptionResponse.Data
                {
                    LogLevel = LogLevel.Error,
                    StatusCode = 403
                }
            },
            { typeof(ExceptionResponse.NotFoundException),
                new ExceptionResponse.Data
                {
                    LogLevel = LogLevel.Error,
                    StatusCode = 404
                }
            },
            { typeof(ExceptionResponse.BadRequestException),
                new ExceptionResponse.Data
                {
                    LogLevel = LogLevel.Error,
                    StatusCode = 400
                }
            },
        };

        private readonly ExceptionResponse.Data _defaultDataExcption = new ExceptionResponse.Data
        {
            LogLevel = LogLevel.Error,
            StatusCode = 500,
        };

        public ExceptionResponse.Data GetExceptionResponseData(Exception? exception)
        {
            if(exception == null)
                return _defaultDataExcption;

            bool foundMapping = _dataExceptionMapping.TryGetValue(exception.GetType(), out ExceptionResponse.Data? data);
            if (foundMapping && data != null)
                return data;

            return _defaultDataExcption;
        }
    }
}
