namespace ServicesAPI.Domain
{
    public class Service
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Price { get; set; }
        public bool Status { get; set; }
        public Guid SpecializationId { get; set; }
        public Specialization? Specialization { get; set; }
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        public Service() { }
        public Service(string name, int price, bool status, Specialization specialization, Category category)
        {
            Name = name;
            Price = price;
            Status = status;
            Specialization = specialization;
            SpecializationId = specialization.Id;
            Category = category;
            CategoryId = category.Id;
        }

        public Service(string name, int price, bool status, Guid specializationId, Guid categoryId)
        {
            Name = name;
            Price = price;
            Status = status;
            SpecializationId = specializationId;
            CategoryId = categoryId;
        }
    }
}
