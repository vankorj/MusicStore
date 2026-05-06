using System.Web.Mvc;
using System.Collections.Generic;
using MusicStore.Domain.Abstract;
using System.Linq;

namespace MusicStore.WebUI.Controllers
{
	public class NavController : Controller
	{
		private IInstrumentRepository repository;

		public NavController(IInstrumentRepository repo)
		{
			repository = repo;
		}

	public PartialViewResult Menu(string category = null)
		{
			ViewBag.SelectedCategory = category;

			IEnumerable<string> categories = repository.Instruments
				.Select(x => x.Category)
				.Distinct()
				.OrderBy(x => x);

			return PartialView(categories);
		}
	}
}