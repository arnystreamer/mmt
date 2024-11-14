using Jimx.MMT.API.Models.Common;

namespace Jimx.MMT.API.Models.StaticItems;

public record LocalSectionApi(int Id, Guid UserId, string Name, string? Description) : DictionaryItemWithDescription(Id, Name, Description);
