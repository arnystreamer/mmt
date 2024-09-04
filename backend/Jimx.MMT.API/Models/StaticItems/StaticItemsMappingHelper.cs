using Jimx.MMT.API.Context;

namespace Jimx.MMT.API.Models.StaticItems
{
	public static class StaticItemsMappingHelper
	{
		public static CategoryApi ToCategoryApi(this Category category)
		{
			if (category == null)
			{
				throw new ArgumentNullException(nameof(category));
			}

			return new CategoryApi(category.Id, category.SectionId, category.Name, category.Description);
		}

		public static SectionApi ToSectionApi(this Section section)
		{
			if (section == null)
			{
				throw new ArgumentNullException(nameof(section));
			}

			return new SectionApi(section.Id, section.Name, section.Description);
		}

		public static GlobalSectionApi ToGlobalSectionApi(this Section section)
		{
			if (section == null)
			{
				throw new ArgumentNullException(nameof(section));
			}

			if (section.WalletId != null || section.UserId != null || section.SharedAccountId != null)
			{
				throw new InvalidOperationException("Section is not global");
			}

			return new GlobalSectionApi(section.Id, section.Name, section.Description);
		}

		public static LocalSectionApi ToLocalSectionApi(this Section section)
		{
			if (section == null)
			{
				throw new ArgumentNullException(nameof(section));
			}

			if (section.WalletId != null || section.UserId == null || section.SharedAccountId != null)
			{
				throw new InvalidOperationException("Section is not local");
			}

			return new LocalSectionApi(section.Id, section.UserId.Value, section.Name, section.Description);
		}

		public static SharedAccountSectionApi ToSharedAccountSectionApi(this Section section)
		{
			if (section == null)
			{
				throw new ArgumentNullException(nameof(section));
			}

			if (section.WalletId != null || section.UserId != null || section.SharedAccountId == null)
			{
				throw new InvalidOperationException("Section is for shared account");
			}

			return new SharedAccountSectionApi(section.Id, section.SharedAccountId.Value, section.Name, section.Description);
		}

		public static WalletSectionApi ToWalletSectionApi(this Section section)
		{
			if (section == null)
			{
				throw new ArgumentNullException(nameof(section));
			}

			if (section.WalletId == null || section.UserId != null || section.SharedAccountId != null)
			{
				throw new InvalidOperationException("Section is not local");
			}

			return new WalletSectionApi(section.Id, section.WalletId.Value, section.Name, section.Description);
		}

		public static UserApi ToUserApi(this User user)
		{
			if (user == null) 
			{
				throw new ArgumentNullException(nameof(user));
			}

			return new UserApi(user.Id, user.Login, user.Name);
		}

		public static SharedAccountApi ToSharedAccountApi(this SharedAccount sharedAccount)
		{
			if (sharedAccount == null)
			{
				throw new ArgumentNullException(nameof(sharedAccount));
			}

			return new SharedAccountApi(sharedAccount.Id, sharedAccount.Name, sharedAccount.Description);
		}
	}
}
