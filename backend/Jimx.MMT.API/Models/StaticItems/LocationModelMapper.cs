using Jimx.MMT.API.Context;
using Jimx.MMT.API.Services.DbWrapper;

namespace Jimx.MMT.API.Models.StaticItems;

public class LocationModelMapper : IModelMapper<LocationApi, LocationEditApi, Location>
{
	public LocationApi MapToApi(Location entity)
	{
		return new LocationApi(entity.Id, entity.CountryCode, entity.LocationCode, entity.Name);
	}

	public void MapToEntity(LocationEditApi editApi, ref Location entity, AdditionalAssignmentsAction<Location>? additionalAssignments = null)
	{
		entity.CountryCode = editApi.CountryCode;
		entity.LocationCode = editApi.LocationCode;
		entity.Name = editApi.Name;

		additionalAssignments?.Invoke(ref entity);
	}
}

