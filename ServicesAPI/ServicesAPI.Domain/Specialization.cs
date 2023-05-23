namespace ServicesAPI.Domain
{
    public class Specialization
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<Service> Services { get; private set; }

        public Specialization() { Services = new List<Service>(); }
        public Specialization(string name, bool isActive)
        {
            Name = name;
            IsActive = isActive;
            Services = new List<Service>();
        }
    }
}
