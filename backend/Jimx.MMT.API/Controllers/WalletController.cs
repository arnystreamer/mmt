﻿using Jimx.MMT.API.App;
using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Common;
using Jimx.MMT.API.Models.StaticItems;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Jimx.MMT.API.Controllers
{
	[ApiController]
	[Route("wallet")]
	public class WalletController : ControllerBase
	{
		private readonly ApiDbContext _context;
		private readonly ILogger<WalletController> _logger;

		public WalletController(ILogger<WalletController> logger, ApiDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet("{id}")]
		public WalletApi Get(int id)
		{
			var wallet = _context.Wallets.FirstOrDefault(c => c.Id == id);
			if (wallet == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			return new WalletApi(wallet.Id, wallet.UserId, wallet.Name, wallet.Description);
		}

		[HttpGet]
		public CollectionApi<WalletApi> GetAll([FromQuery] CollectionRequestApi requestApi)
		{
			var count = _context.Wallets.Count();

			int skip = requestApi.Skip ?? 0;
			int take = requestApi.Take ?? 10;
			var wallets = _context.Wallets.Skip(skip).Take(take).ToList();

			IList<WalletApi> result = new List<WalletApi>();
			foreach (var wallet in wallets)
			{
				result.Add(new WalletApi(wallet.Id, wallet.UserId, wallet.Name, wallet.Description));
			}

			return new CollectionApi<WalletApi>(count, skip, take, result.Count, result.ToArray());
		}

		[HttpPost]
		public WalletApi Post(WalletApi walletApi)
		{
			var currentUserId = Guid.Empty;

			var entry = _context.Wallets.Add(new Wallet()
			{
				UserId = currentUserId,
				Name = walletApi.Name,
				Description = walletApi.Description
			});

			_context.SaveChanges();

			Wallet entity = entry.Entity;
			return new WalletApi(entry.Entity.Id, entity.UserId, entry.Entity.Name, entry.Entity.Description);
		}

		[HttpPut]
		public WalletApi Put(WalletApi walletApi)
		{
			var currentUserId = Guid.Empty;

			var wallet = _context.Wallets.FirstOrDefault(c => c.Id == walletApi.Id);
			if (wallet == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(walletApi.Id), typeof(IdItem));
			}

			if (wallet.UserId != currentUserId)
			{
				throw new StatusCodeException(HttpStatusCode.Forbidden, new IdItem(walletApi.Id), typeof(IdItem));
			}

			wallet.Name = walletApi.Name;
			wallet.Description = walletApi.Description;
			_context.SaveChanges();

			return walletApi;
		}

		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			var currentUserId = Guid.Empty;

			var wallet = _context.Wallets.FirstOrDefault(c => c.Id == id);
			if (wallet == null)
			{
				throw new StatusCodeException(HttpStatusCode.NotFound, new IdItem(id), typeof(IdItem));
			}

			if (wallet.UserId != currentUserId)
			{
				throw new StatusCodeException(HttpStatusCode.Forbidden, new IdItem(id), typeof(IdItem));
			}

			_context.Wallets.Remove(wallet);
			_context.SaveChanges();

			return;
		}

		[HttpGet("{id}/sections")]
		public WalletSectionsApi GetSections(int id)
		{
			throw new NotImplementedException();
		}
	}
}
