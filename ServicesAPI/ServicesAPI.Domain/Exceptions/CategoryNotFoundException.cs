namespace ServicesAPI.Domain.Exceptions
{
    public class CategoryNotFoundException : NotFoundException
    {
        public CategoryNotFoundException(Guid id)
            : base($"Service category with ID = {id} does not exist")
        {
        }
        public CategoryNotFoundException(string name)
            : base($"Service category with Name = {name} does not exist")
        {
        }
    }
}
