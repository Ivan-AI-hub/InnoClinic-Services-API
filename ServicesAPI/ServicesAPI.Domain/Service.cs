namespace ServicesAPI.Domain
{
    public class Service
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Price { get; set; }
        public bool Status { get; set; }
        public Guid SpecializationId { get; private set; }
        public Specialization? Specialization { get; set; }
        public Guid CategoryId { get; private set; }
        public ServiceCategory? Category { get; set; }

        private Service() { }
        public Service(string name, string price, bool status, Specialization specialization, ServiceCategory category)
        {
            Name = name;
            Price = price;
            Status = status;
            Specialization = specialization;
            SpecializationId = specialization.Id;
            Category = category;
            CategoryId = category.Id;
        }

        public Service(string name, string price, bool status, Guid specializationId, Guid categoryId)
        {
            Name = name;
            Price = price;
            Status = status;
            SpecializationId = specializationId;
            CategoryId = categoryId;
        }
    }
}
