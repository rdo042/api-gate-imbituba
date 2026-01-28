namespace GateAPI.Application.Common.Exceptions
{
    public class ExternalServiceException : Exception
    {
        public ExternalServiceException(string message) : base(message) { }
    }

    public class ExternalServiceBadRequestException : ExternalServiceException
    {
        public ExternalServiceBadRequestException(string message) : base(message) { }
    }

    public class ExternalServiceUnauthorizedException : ExternalServiceException
    {
        public ExternalServiceUnauthorizedException(string message) : base(message) { }
    }

    public class ExternalServiceForbiddenException : ExternalServiceException
    {
        public ExternalServiceForbiddenException(string message) : base(message) { }
    }

    public class ExternalServiceNotFoundException : ExternalServiceException
    {
        public ExternalServiceNotFoundException(string message) : base(message) { }
    }

    public class ExternalServiceInternalErrorException : ExternalServiceException
    {
        public ExternalServiceInternalErrorException(string message) : base(message) { }
    }

    public class ExternalServiceUnavailableException : ExternalServiceException
    {
        public ExternalServiceUnavailableException(string message) : base(message) { }
    }

}
