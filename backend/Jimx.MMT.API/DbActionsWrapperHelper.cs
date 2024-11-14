using Jimx.MMT.API.Context;
using Jimx.MMT.API.Controllers;
using Jimx.MMT.API.Models.Receipt;
using Jimx.MMT.API.Models.StaticItems;
using Jimx.MMT.API.Services.DbWrapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Jimx.MMT.API
{
    public static class DbActionsWrapperHelper
	{
		public static IServiceCollection RegisterAllDbActionsWrappers(this IServiceCollection services)
		{
			services.RegisterProxyDbActionsWrapper<CurrencyApi, CurrencyEditApi, Currency, CurrencyModelMapper>(c => c.Currencies);
			services.RegisterProxyDbActionsWrapper<LocationApi, LocationEditApi, Location, LocationModelMapper>(c => c.Locations);

			services.AddScoped<IRepository<Section>>(sp => new ProxyRepository<Section>(sp.GetRequiredService<ApiDbContext>(), c => c.Sections));
			services.RegisterProxyDbActionsWrapper<GlobalSectionApi, SectionEditApi, Section, GlobalSectionModelMapper>();
			services.RegisterProxyDbActionsWrapper<LocalSectionApi, SectionEditApi, Section, LocalSectionModelMapper>();
			services.RegisterProxyDbActionsWrapper<SharedAccountSectionApi, SectionEditApi, Section, SharedAccountSectionModelMapper>();
			services.RegisterProxyDbActionsWrapper<WalletSectionApi, SectionEditApi, Section, WalletSectionModelMapper>();
			services.RegisterProxyDbActionsWrapper<SectionApi, SectionEditApi, Section, SectionModelMapper>();

			services.RegisterProxyDbActionsWrapper<CategoryApi, CategoryEditApi, Category, CategoryModelMapper>(c => c.Categories);

			services.RegisterProxyDbActionsWrapper<ProductApi, ProductEditApi, Product, ProductModelMapper>(c => c.Products);
			services.RegisterProxyDbActionsWrapper<ReceiptApi, ReceiptEditApi, Receipt, ReceiptModelMapper>(c => c.Receipts);
			services.RegisterProxyDbActionsWrapper<ReceiptEntryApi, ReceiptEntryEditApi, ReceiptEntry, ReceiptEntryModelMapper>(c => c.ReceiptEntries);

			services.RegisterProxyDbActionsWrapper<UserApi, UserEditApi, User, UserModelMapper>(c => c.Users);
			services.AddScoped<UserActionsWrapper>();


			services.RegisterProxyDbActionsWrapper<SharedAccountApi, SharedAccountEditApi, SharedAccount, SharedAccountModelMapper>(c => c.SharedAccounts);
			services.RegisterProxyDbActionsWrapper<SharedAccountToUserApi, SharedAccountToUserEditApi, SharedAccountToUser, SharedAccountToUserModelMapper>(
				c => c.SharedAccountToUsers);

			services.RegisterProxyDbActionsWrapper<WalletApi, WalletEditApi, Wallet, WalletModelMapper>(c => c.Wallets);

			return services;
		}

		public static IServiceCollection RegisterProxyDbActionsWrapper<TApi, TEditApi, TEntity, TModelMapper>(this IServiceCollection services,
			Func<ApiDbContext, DbSet<TEntity>> entitiesSelector)
			where TApi : class
			where TEditApi : class
			where TEntity : class, new()
			where TModelMapper : class, IModelMapper<TApi, TEditApi, TEntity>
		{
			return services
				.AddScoped<IRepository<TEntity>>(sp => new ProxyRepository<TEntity>(sp.GetRequiredService<ApiDbContext>(), entitiesSelector))
				.RegisterProxyDbActionsWrapper<TApi, TEditApi, TEntity, TModelMapper>();
		}

		public static IServiceCollection RegisterProxyDbActionsWrapper<TApi, TEditApi, TEntity, TModelMapper>(this IServiceCollection services)
			where TApi : class
			where TEditApi : class
			where TEntity : class, new()
			where TModelMapper : class, IModelMapper<TApi, TEditApi, TEntity>
		{
			return services
				.AddTransient<DbActionsWrapper<TApi, TEditApi, TEntity>>()
				.AddTransient<IModelMapper<TApi, TEditApi, TEntity>, TModelMapper>()
				.AddTransient<IGetModelMapper<TEntity, TApi>, TModelMapper>()
				.AddTransient<IEditEntityMapper<TEntity, TEditApi>, TModelMapper>();
		}
	}
}
