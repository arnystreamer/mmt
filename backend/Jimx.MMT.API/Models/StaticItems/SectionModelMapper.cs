using Jimx.MMT.API.Context;
using Jimx.MMT.API.Services.DbWrapper;

namespace Jimx.MMT.API.Models.StaticItems;

public class SectionModelMapper : IModelMapper<SectionApi, SectionEditApi, Context.Section>
{
	public SectionApi MapToApi(Section entity)
	{
		return new SectionApi(entity.Id, entity.Name, entity.Description);
	}

	public void MapToEntity(SectionEditApi editApi, ref Section entity, AdditionalAssignmentsAction<Section>? additionalAssignments = null)
	{
		entity.Name = editApi.Name;
		entity.Description = editApi.Description;

		additionalAssignments?.Invoke(ref entity);
	}
}
