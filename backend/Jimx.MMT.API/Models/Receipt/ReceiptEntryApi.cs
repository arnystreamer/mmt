using Jimx.MMT.API.Models.StaticItems;

namespace Jimx.MMT.API.Models.Receipt;

public record ReceiptEntryApi(Guid Id, Guid ProductId,
	ProductApi? Product,
	decimal Quantity, decimal Price, 
    DateTime CreateTime, 
    Guid CreateUserId, UserApi? CreateUser) :
        ReceiptEntryEditApi(ProductId, Quantity, Price, CreateTime);

