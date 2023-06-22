namespace ServicesAPI.Presentation.Models.RequestModels
{
    public record EditServiceRequestModel(string Name, int Price, bool Status, Guid SpecializationId, string CategoryName);
}
