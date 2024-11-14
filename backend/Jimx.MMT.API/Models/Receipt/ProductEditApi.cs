namespace Jimx.MMT.API.Models.Receipt;

public record ProductEditApi(Guid? ParentId, string Name, string Description, int? CategoryId, int? SectionId);
