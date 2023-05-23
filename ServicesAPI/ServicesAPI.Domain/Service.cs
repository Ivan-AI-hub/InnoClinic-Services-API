namespace ServicesAPI.Domain
{
    public class Service
    {
        private Category? _category;
        private Specialization? _specialization;
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Price { get; set; }
        public bool Status { get; set; }
        public Guid SpecializationId { get; set; }
        public Guid CategoryId { get; set; }
        public Specialization? Specialization
        {
            get => _specialization;
            set { _specialization = value; SpecializationId = value != null ? value.Id : SpecializationId; }
        }
        public Category? Category
        {
            get => _category;
            set { _category = value; CategoryId = value != null ? value.Id : CategoryId; }
        }

        public Service() { }
        public Service(string name, int price, bool status, Specialization specialization, Category category)
        {
            Name = name;
            Price = price;
            Status = status;
            Specialization = specialization;
            Category = category;
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
