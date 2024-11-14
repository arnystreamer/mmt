using Jimx.MMT.API.Context;
using Jimx.MMT.API.Services.DbWrapper;

namespace Jimx.MMT.API.Models.StaticItems;

public class CategoryModelMapper : IModelMapper<CategoryApi, CategoryEditApi, Category>
{
	public CategoryApi MapToApi(Category entity)
	{
		return new CategoryApi(entity.Id, entity.SectionId, entity.Name, entity.Description);
	}

	public void MapToEntity(CategoryEditApi editApi, ref Category entity, AdditionalAssignmentsAction<Category>? additionalAssignments = null)
	{
		entity.Name = editApi.Name;
		entity.Description = editApi.Description;

		additionalAssignments?.Invoke(ref entity);
	}
}
