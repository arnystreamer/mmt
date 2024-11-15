using Jimx.MMT.API.Models.Common;

namespace Jimx.MMT.API.Models.StaticItems
{
	public record SharedAccountToUserApi(int Id, int SharedAccountId, Guid UserId);
}
