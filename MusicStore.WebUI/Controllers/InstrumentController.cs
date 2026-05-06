using MusicStore.Domain.Abstract;
using MusicStore.Domain.Models;
using MusicStore.WebUI.Models;
using System.Linq;
using System.Web.Mvc;

namespace MusicStore.WebUI.Controllers
{
	public class InstrumentController : Controller
	{
		private readonly IInstrumentRepository repository;
		private const int PageSize = 4;

		public InstrumentController(IInstrumentRepository repo)
		{
			repository = repo;
		}

		public ViewResult List(string category, int page = 1)
		{
			if (repository == null)
				throw new System.Exception("IInstrumentRepository was not injected correctly.");

			var query = repository.Instruments
				.Where(g => string.IsNullOrEmpty(category) || g.Category == category)
				.OrderBy(g => g.Id);

			var totalItems = query.Count();

			var pagedInstruments = query
				.Skip((page - 1) * PageSize)
				.Take(PageSize)
				.ToList();

			var model = new InstrumentsListViewModel
			{
				Instruments = pagedInstruments,
				CurrentCategory = category,
				PagingInfo = new PagingInfo
				{
					CurrentPage = page,
					ItemsPerPage = PageSize,
					TotalItems = totalItems
				}
			};

			return View(model);
		}

		[AllowAnonymous]
		public FileContentResult GetImage(int id)
		{
			var Instrument = repository.Instruments
				.FirstOrDefault(g => g.Id == id);

			if (Instrument != null && Instrument.ImageData != null)
			{
				return File(Instrument.ImageData, Instrument.ImageMimeType);
			}

			return null;
		}
	}
}