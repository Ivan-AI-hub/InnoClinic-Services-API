namespace ServicesAPI.Domain.Exceptions
{
    public class ServiceNotFoundException : NotFoundException
    {
        public ServiceNotFoundException(Guid id) 
            : base($"Service with ID ={id} does not exist")
        {
        }
    }
}
