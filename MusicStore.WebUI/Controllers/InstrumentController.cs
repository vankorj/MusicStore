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
		private const int PageSize = 3;

		public InstrumentController(IInstrumentRepository repo)
		{
			repository = repo;
		}

		public ViewResult List(string category, string search, string sort, int page = 1)
		{
			if (repository == null)
				throw new System.Exception("IInstrumentRepository was not injected correctly.");

			// Start with all instruments
			var query = repository.Instruments.AsQueryable();

			// CATEGORY FILTER
			if (!string.IsNullOrEmpty(category))
			{
				query = query.Where(i => i.Category == category);
			}

			// SEARCH FILTER
			if (!string.IsNullOrEmpty(search))
			{
				query = query.Where(i =>
					i.Name.Contains(search) ||
					i.Brand.Contains(search) ||
					i.Category.Contains(search));
			}

			// SORTING
			switch (sort)
			{
				case "price_asc":
					query = query.OrderBy(i => i.Price);
					break;

				case "price_desc":
					query = query.OrderByDescending(i => i.Price);
					break;

				case "name_asc":
					query = query.OrderBy(i => i.Name);
					break;

				case "name_desc":
					query = query.OrderByDescending(i => i.Name);
					break;

				default:
					query = query.OrderBy(i => i.Id);
					break;
			}

			// PAGING
			var totalItems = query.Count();

			var pagedInstruments = query
				.Skip((page - 1) * PageSize)
				.Take(PageSize)
				.ToList();

			// VIEWMODEL
			var model = new InstrumentsListViewModel
			{
				Instruments = pagedInstruments,

				// ⭐ THESE THREE FIX PAGING ⭐
				CurrentCategory = category,
				SearchTerm = search,
				CurrentSort = sort,

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
			var instrument = repository.Instruments
				.FirstOrDefault(g => g.Id == id);

			if (instrument != null && instrument.ImageData != null)
			{
				return File(instrument.ImageData, instrument.ImageMimeType);
			}

			return null;
		}
	}
}
