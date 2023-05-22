using FluentMigrator;

namespace ServicesAPI.Persistence.Migrations
{
    [Migration(202106280001)]
    public class InitialTables_202106280001 : Migration
    {
        public override void Up()
        {
            Create.Table("Categories")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("TimeSlotSize").AsInt32().NotNullable();

            Create.Table("Specializations")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("IsActive").AsBoolean().NotNullable();

            Create.Table("Services")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Price").AsInt32().NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("SpecializationId").AsGuid().NotNullable().ForeignKey("Specializations", "Id")
                .WithColumn("CategoryId").AsGuid().NotNullable().ForeignKey("Categories", "Id");
        }
        public override void Down()
        {
            Delete.Table("Services");
            Delete.Table("Specializations");
            Delete.Table("Categories");
        }
    }
}
