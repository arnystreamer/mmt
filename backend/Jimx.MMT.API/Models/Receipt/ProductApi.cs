using Jimx.MMT.API.Models.StaticItems;

namespace Jimx.MMT.API.Models.Receipt;

public record ProductApi(Guid Id, Guid? ParentId, string Name, string Description,
    int? CategoryId, CategoryApi? Category, int? SectionId, SectionApi? Section, DateTime CreateTime, Guid CreateUserId, UserApi CreateUser) :
    ProductEditApi(ParentId, Name, Description, CategoryId, SectionId);

