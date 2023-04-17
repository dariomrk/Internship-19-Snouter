namespace Data.Interfaces
{
    public interface IEntity<TId>
    {
        TId Id { get; }
    }
}
