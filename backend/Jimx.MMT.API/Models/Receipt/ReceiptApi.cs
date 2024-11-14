using Jimx.MMT.API.Models.StaticItems;

namespace Jimx.MMT.API.Models.Receipt;

public record ReceiptApi(Guid Id, DateTime Date, int LocationId, LocationApi? Location, int CurrencyId, CurrencyApi? Currency, string? Comment,
    DateTime CreateTime, Guid CreateUserId, UserApi? CreateUser) :
    ReceiptEditApi(Date, LocationId, CurrencyId, Comment);

