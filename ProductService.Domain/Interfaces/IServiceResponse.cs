namespace ProductService.Domain.Interfaces
{
    public interface IServiceResponse
    {
        bool Success { get; set; }
        string Message { get; set; }
    }
}
