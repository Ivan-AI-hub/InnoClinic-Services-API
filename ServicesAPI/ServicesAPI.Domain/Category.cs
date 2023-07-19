namespace ServicesAPI.Domain
{
    public class Category
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int TimeSlotSize { get; set; }
        public List<Service> Services { get; private set; }
        public Category() { Services = new List<Service>(); }
        public Category(string name, int timeSlotSize)
        {
            Name = name;
            TimeSlotSize = timeSlotSize;
            Services = new List<Service>();
        }
    }
}
