namespace Jimx.MMT.API.Models.Receipt;

public record ProductEditApi(Guid? ParentId, bool IsExclusiveForCurrentUser, string Name, string? Description, int SectionId, int? CategoryId);
