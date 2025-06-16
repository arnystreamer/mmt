using Jimx.Common.Models;

namespace Jimx.MMT.API.Models.StaticItems;

public class SharedAccountUsersApi(int id, string name, string? description, UserApi[] users) : DictionaryItemWithDescription(id, name, description)
{
    public UserApi[] Users { get; init; } = users;
}
