using Jimx.MMT.API.Context;
using Jimx.MMT.API.Services.DbWrapper;

namespace Jimx.MMT.API.Models.StaticItems;

public class CurrencyModelMapper : IModelMapper<CurrencyApi, CurrencyEditApi, Currency>
{
	public CurrencyApi MapToApi(Currency entity)
	{
		return new CurrencyApi(entity.Id, entity.Code, entity.Name);
	}

	public void MapToEntity(CurrencyEditApi editApi, ref Currency entity, AdditionalAssignmentsAction<Currency>? additionalAssignments = null)
	{
		entity.Code = editApi.Code;
		entity.Name = editApi.Name;

		additionalAssignments?.Invoke(ref entity);
	}
}

