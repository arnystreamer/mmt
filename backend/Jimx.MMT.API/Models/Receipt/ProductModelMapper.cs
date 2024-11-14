using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.StaticItems;
using Jimx.MMT.API.Services.DbWrapper;

namespace Jimx.MMT.API.Models.Receipt;

public class ProductModelMapper : IModelMapper<ProductApi, ProductEditApi, Product>
{
	public ProductApi MapToApi(Product entity)
	{
		return new ProductApi(entity.Id, entity.ParentId, entity.Name, entity.Description, entity.CategoryId,
			entity.Category != null ? new CategoryApi(entity.Category.Id, entity.Category.SectionId, entity.Category.Name, entity.Category.Description) : null,
			entity.SectionId,
			entity.Section != null ? new SectionApi(entity.Section.Id, entity.Section.Name, entity.Section.Description) : null,
			entity.CreateTime,
			entity.CreateUserId,
			new UserApi(entity.CreateUser.Id, entity.CreateUser.Login, entity.CreateUser.Name));
	}

	public void MapToEntity(ProductEditApi editApi, ref Product entity, AdditionalAssignmentsAction<Product>? additionalAssignments = null)
	{
		entity.ParentId = editApi.ParentId;
		entity.Name = editApi.Name;
		entity.Description = editApi.Description;
		entity.CategoryId = editApi.CategoryId;
		entity.SectionId = editApi.SectionId;

		additionalAssignments?.Invoke(ref entity);
	}
}
