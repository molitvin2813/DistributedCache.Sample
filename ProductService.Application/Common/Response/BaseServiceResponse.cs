using ProductService.Domain.Interfaces;

namespace ProductService.Application.Common.Response
{
    public abstract class BaseServiceResponse : IServiceResponse
    {
        public BaseServiceResponse()
        {
            Success = true;
            Message = string.Empty;
        }

        public BaseServiceResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }

        public string Message { get; set; }
    }
}
