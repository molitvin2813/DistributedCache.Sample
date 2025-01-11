namespace ProductService.Application.Common.Response
{
    public class ServiceResponseWrite : BaseServiceResponse
    {
        public ServiceResponseWrite() : base() { }

        public ServiceResponseWrite(bool success, string message) : base(success, message) { }
    }
}
