namespace Hydrogen.Repo.Abstractions
{
    public interface IHydrogenModel<out TId>
    {
        public TId Id { get; }
    }
}