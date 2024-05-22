using Jimx.MMT.API.Models.Common;
using System.Xml.Linq;

namespace Jimx.MMT.API.Models.StaticItems
{
	public record SectionApi(int Id, int WalletId, string Name, string Description) : DictionaryItemWithDescription(Id, Name, Description);
}
