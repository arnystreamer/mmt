namespace Jimx.MMT.API.Models.StaticItems;

public record CurrencyApi(int Id, string Code, string Name) : CurrencyEditApi(Code, Name);

