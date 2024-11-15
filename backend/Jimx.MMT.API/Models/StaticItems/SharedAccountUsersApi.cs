using Jimx.MMT.API.Models.Common;

namespace Jimx.MMT.API.Models.StaticItems;

public record SharedAccountUsersApi(int Id, string Name, string? Description, UserApi[] Users) : DictionaryItemWithDescription(Id, Name, Description);
