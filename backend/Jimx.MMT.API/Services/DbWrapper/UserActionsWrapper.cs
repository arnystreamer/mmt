using Jimx.MMT.API.Context;
using Jimx.MMT.API.Controllers;
using Jimx.MMT.API.Models.StaticItems;

namespace Jimx.MMT.API.Services.DbWrapper
{
	public class UserActionsWrapper : DbActionsWrapper<UserApi, UserEditApi, User>
	{
		public UserActionsWrapper(
			IRepository<User> dbSetDriver,
			IGetModelMapper<User, UserApi> getModelMapper,
			IEditEntityMapper<User, UserEditApi> editEntityMapper)
			: base(dbSetDriver, getModelMapper, editEntityMapper)
		{

		}

		public User? GetByLogin(string login)
		{
			return Repository.StartRequest(true).Get(u => u.Login.ToLower() == login);
		}
	}
}
