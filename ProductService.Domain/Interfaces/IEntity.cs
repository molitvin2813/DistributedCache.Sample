namespace ProductService.Domain.Interfaces
{
    public interface IEntity<T>
        where T : struct
    {
        T Id { get; set; }
    }
}
