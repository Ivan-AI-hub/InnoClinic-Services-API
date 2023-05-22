namespace ServicesAPI.Domain.Exceptions
{
    public class SpecializationNotFoundException : NotFoundException
    {
        public SpecializationNotFoundException(Guid id)
            : base($"Specialization with ID ={id} does not exist")
        {
        }
    }
}
