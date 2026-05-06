using MusicStore.Domain.Abstract;
using MusicStore.Domain.Concrete;
using MusicStore.Domain.Infrastructure.Concrete;
using MusicStore.Domain.Models;
using MusicStore.Domain.Abstract;
using Moq;
using Ninject;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace MusicStore.WebUI.Infrastructure
{
	public class NinjectDependencyResolver : IDependencyResolver
	{
		private IKernel kernel;

		public NinjectDependencyResolver(IKernel kernelParam)
		{
			kernel = kernelParam;
			AddBindings();
		}

		public object GetService(System.Type serviceType)
		{
			return kernel.TryGet(serviceType);
		}

		public IEnumerable<object> GetServices(System.Type serviceType)
		{
			return kernel.GetAll(serviceType);
		}

		private void AddBindings()
		{
			// Existing binding
			kernel.Bind<IInstrumentRepository>()
				  .To<EFInstrumentRepository>();

			// Email settings
			kernel.Bind<EmailSettings>()
				.ToSelf()
				.InSingletonScope()
				.WithConstructorArgument("settings", new EmailSettings
				{
					WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"]),
					FileLocation = ConfigurationManager.AppSettings["Email.FileLocation"]
				});

			// Order processor
			kernel.Bind<IOrderProcessor>()
				  .To<EmailOrderProcessor>();

			// =========================
			// AUTH PROVIDER (ADD THIS)
			// =========================
			kernel.Bind<IAuthProvider>()
				  .To<FormsAuthProvider>();
		}
	}
}