namespace Jimx.MMT.API.Models.StaticItems;

public record LocationApi(int Id, string CountryCode, string LocationCode) :
	LocationEditApi(CountryCode, LocationCode);

