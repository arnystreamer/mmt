using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.StaticItems;
using Jimx.MMT.API.Services.DbWrapper;
using Microsoft.EntityFrameworkCore;

namespace Jimx.MMT.API.Models.Receipt;

public class ReceiptEntryModelMapper : IModelMapper<ReceiptEntryApi, ReceiptEntryEditApi, ReceiptEntry>,
	ICustomDbSetConvertationProvider<Context.ReceiptEntry, ReceiptEntryApi>
{
	public IQueryable<Context.ReceiptEntry> DbSetToQueryable(DbSet<Context.ReceiptEntry> dbSet)
	{
		return dbSet
			.Include(r => r.Product).ThenInclude(p => p.Section)
			.Include(r => r.Product).ThenInclude(p => p.Category).ThenInclude(c => c.Section)
			.Include(r => r.Product).ThenInclude(p => p.CreateUser)
			.Include(r => r.CreateUser);
	}

	public ReceiptEntryApi MapToApi(ReceiptEntry entity)
	{
		return MapFromReceiptEntryToApi(entity);
	}

	public static ReceiptEntryApi MapFromReceiptEntryToApi(ReceiptEntry entity)
	{
		return new ReceiptEntryApi(entity.Id, entity.ProductId,
			entity.Product != null 
			? new ProductApi(entity.Product.Id, entity.Product.ParentId,
				entity.Product.UserId.HasValue,
				entity.Product.Name, entity.Product.Description,
				entity.Product.SectionId,
				entity.Product.Section != null 
					? new SectionApi(entity.Product.Section.Id, SectionType.Unknown, entity.Product.Section.Name, entity.Product.Section.Description) 
					: null,
				entity.Product.CategoryId,
				entity.Product.Category != null
				? new CategoryApi(
					entity.Product.Category.Id,
					entity.Product.Category.SectionId,
					entity.Product.Category.Section != null 
						? new SectionApi(entity.Product.Category.Section.Id, SectionType.Unknown, entity.Product.Category.Section.Name, entity.Product.Category.Section.Description) 
						: null,
					entity.Product.Category.Name,
					entity.Product.Category.Description)
				: null,
				entity.Product.CreateTime,
				entity.Product.CreateUserId,
				entity.Product.CreateUser != null ? new UserApi(entity.Product.CreateUser.Id, entity.Product.CreateUser.Login, entity.Product.CreateUser.Name) : null)
			: null,
			entity.Quantity, entity.Price, entity.CreateTime, entity.CreateUserId,
			entity.CreateUser != null ? new UserApi(entity.CreateUser.Id, entity.CreateUser.Login, entity.CreateUser.Name) : null);
	}

	public void MapToEntity(ReceiptEntryEditApi editApi, ref ReceiptEntry entity, AdditionalAssignmentsAction<ReceiptEntry>? additionalAssignments = null)
	{
		entity.ProductId = editApi.ProductId;
		entity.Quantity = editApi.Quantity;
		entity.Price = editApi.Price;

		additionalAssignments?.Invoke(ref entity);
	}
}
