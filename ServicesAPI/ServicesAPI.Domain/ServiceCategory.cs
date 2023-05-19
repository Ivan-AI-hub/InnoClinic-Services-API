namespace ServicesAPI.Domain
{
    public class ServiceCategory
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int TimeSlotSize { get; set; }
        public List<Service> Services { get; private set; }
        private ServiceCategory() { }
        public ServiceCategory(string name, int timeSlotSize)
        {
            Name = name;
            TimeSlotSize = timeSlotSize;
            Services = new List<Service>();
        }
    }
}
