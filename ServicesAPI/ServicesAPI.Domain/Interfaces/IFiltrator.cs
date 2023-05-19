namespace ServicesAPI.Domain.Interfaces
{
    public interface IFiltrator<T>
    {
        public IQueryable<T> Filtrate(IQueryable<T> query);
    }
}
