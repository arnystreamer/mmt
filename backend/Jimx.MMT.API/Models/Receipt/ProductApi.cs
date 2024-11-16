using Jimx.MMT.API.Models.StaticItems;

namespace Jimx.MMT.API.Models.Receipt;

public record ProductApi(Guid Id, Guid? ParentId, bool IsExclusiveForUser, string Name, string? Description,
	int SectionId, SectionApi? Section,
	int? CategoryId, CategoryApi? Category, 
	DateTime CreateTime, 
	Guid CreateUserId, UserApi? CreateUser) :
		ProductEditApi(ParentId, IsExclusiveForUser, Name, Description, SectionId, CategoryId, CreateTime);

