namespace ServicesAPI.Domain
{
    public class Specialization
    {
        public Guid Id { get; private set; } = new Guid();
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<Service> Services { get; private set; }

        private Specialization() { }
        public Specialization(string name, bool isActive)
        {
            Name = name;
            IsActive = isActive;
            Services = new List<Service>();
        }
    }
}
