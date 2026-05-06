using MusicStore.Domain.Abstract;
using System.Web.Security;

namespace MusicStore.Domain.Infrastructure.Concrete
{
	public class FormsAuthProvider : IAuthProvider
	{
		public bool Authenticate(string username, string password)
		{
			return username == "admin" && password == "secret";
		}

		public void SetAuthCookie(string username)
		{
			FormsAuthentication.SetAuthCookie(username, false);
		}

		public void SignOut()
		{
			FormsAuthentication.SignOut();
		}
	}
}