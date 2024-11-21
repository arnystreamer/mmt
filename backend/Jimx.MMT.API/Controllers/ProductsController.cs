using Jimx.MMT.API.App;
using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Common;
using Jimx.MMT.API.Models.Receipt;
using Jimx.MMT.API.Models.StaticItems;
using Jimx.MMT.API.Services.Auth;
using Jimx.MMT.API.Services.DbWrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;

namespace Jimx.MMT.API.Controllers
{
    [Route("products")]
	[ApiController]
	[Authorize]
	public class ProductsController : ControllerBase
	{
		private readonly ILogger<ProductsController> _logger;
		private readonly DbActionsWrapper<ProductApi, ProductEditApi, Product> _wrapper;
		private readonly DbActionsWrapper<SectionApi, SectionEditApi, Section> _sectionWrapper;
		private readonly UserActionsWrapper _usersWrapper;

		private readonly Func<Guid, Expression<Func<Product, bool>>> ExpressionIsProductBelongsUser =
			userId =>
			p => (p.Section.UserId == null && p.Section.WalletId == null && p.Section.SharedAccountId == null)
					|| p.Section.UserId == userId
					|| (p.Section.Wallet != null && p.Section.Wallet.UserId == userId)
					|| (p.Section.SharedAccount != null && p.Section.SharedAccount.SharedAccountToUsers.Any(satu => satu.UserId == userId))
					&& (p.UserId == null || p.UserId == userId)
					&& !p.IsDeleted;

		private readonly Func<Guid, Expression<Func<Section, bool>>> ExpressionIsSectionBelongsUser =
			userId =>
			s => (s.UserId == null && s.WalletId == null && s.SharedAccountId == null)
					|| s.UserId == userId
					|| (s.Wallet != null && s.Wallet.UserId == userId)
					|| (s.SharedAccount != null && s.SharedAccount.SharedAccountToUsers.Any(satu => satu.UserId == userId));

		public ProductsController(ILogger<ProductsController> logger, 
			DbActionsWrapper<ProductApi, ProductEditApi, Product> wrapper,
			DbActionsWrapper<SectionApi, SectionEditApi, Section> sectionWrapper,
			UserActionsWrapper usersWrapper)
		{
			_logger = logger;
			_wrapper = wrapper;
			_sectionWrapper = sectionWrapper;
			_usersWrapper = usersWrapper;
		}

		[HttpGet]
		public CollectionApi<ProductApi> GetAll([FromQuery] CollectionRequestApi requestApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			return _wrapper.GetAll(requestApi, ExpressionIsProductBelongsUser(currentUser.Id));
		}

		[HttpGet("{id}")]
		public ProductApi Get(Guid id)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var currency = _wrapper.Get(p => p.Id  == id, ExpressionIsProductBelongsUser(currentUser.Id));

			if (currency == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new GuidItem(id), typeof(GuidItem));
			}

			return currency;
		}

		[HttpPost]
		public ProductApi Post(ProductEditApi productApi)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var section = _sectionWrapper.Get(s => s.Id == productApi.SectionId, ExpressionIsSectionBelongsUser(currentUser.Id));

			if (section == null)
			{
				throw new StatusCodeException(HttpStatusCode.BadRequest, new IdItem(productApi.SectionId), typeof(IdItem));
			}

			if (!productApi.IsExclusiveForCurrentUser && (section.Type == SectionType.Local || section.Type == SectionType.Wallet))
			{
				throw new StatusCodeException(HttpStatusCode.BadRequest, 
					new StringItem($"Section {section.Type} is more private than Product, set IsExclusiveForCurrentUser to true"), 
					typeof(StringItem));
			}

			var result = _wrapper.Add(productApi, (ref Product p) =>
			{
				p.CreateTime = DateTime.Now.ToUniversalTime();
				p.CreateUserId = currentUser.Id;
				p.UserId = productApi.IsExclusiveForCurrentUser ? currentUser.Id : null;
			});

			return _wrapper.Get(p => p.Id == result.Id, ExpressionIsProductBelongsUser(currentUser.Id)) 
				?? throw new InvalidOperationException($"Added item expected to have id = {result.Id} but not found");
		}

		[HttpDelete("{id}")]
		public void Delete(Guid id)
		{
			var currentUser = _usersWrapper.GetCurrentUserFromContext(User);

			var result = _wrapper.Delete(p => p.Id == id, ExpressionIsProductBelongsUser(currentUser.Id));

			if (!result)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new GuidItem(id), typeof(GuidItem));
			}
		}
	}
}
