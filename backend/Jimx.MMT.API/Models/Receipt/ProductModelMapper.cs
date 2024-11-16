using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.StaticItems;
using Jimx.MMT.API.Services.DbWrapper;

namespace Jimx.MMT.API.Models.Receipt;

public class ProductModelMapper : IModelMapper<ProductApi, ProductEditApi, Product>
{
	public ProductApi MapToApi(Product entity)
	{
		return new ProductApi(entity.Id, entity.ParentId, 
			entity.UserId.HasValue,
			entity.Name, entity.Description, 
			entity.SectionId,
			entity.Section != null ? new SectionApi(entity.Section.Id, SectionType.Unknown, entity.Section.Name, entity.Section.Description) : null,
			entity.CategoryId,
			entity.Category != null 
			? new CategoryApi(
				entity.Category.Id, 
				entity.Category.SectionId,  
				entity.Category.Section != null ? new SectionApi(entity.Category.Section.Id, SectionType.Unknown, entity.Category.Section.Name, entity.Category.Section.Description) : null,
				entity.Category.Name, 
				entity.Category.Description) 
			: null,
			entity.CreateTime,
			entity.CreateUserId,
			entity.CreateUser != null ? new UserApi(entity.CreateUser.Id, entity.CreateUser.Login, entity.CreateUser.Name) : null);
	}

	public void MapToEntity(ProductEditApi editApi, ref Product entity, AdditionalAssignmentsAction<Product>? additionalAssignments = null)
	{
		entity.ParentId = editApi.ParentId;
		entity.Name = editApi.Name;
		entity.Description = editApi.Description;
		entity.SectionId = editApi.SectionId;
		entity.CategoryId = editApi.CategoryId;

		additionalAssignments?.Invoke(ref entity);
	}
}
