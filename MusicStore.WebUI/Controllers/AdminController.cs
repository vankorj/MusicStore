using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Domain.Abstract;
using MusicStore.Domain.Models;

namespace MusicStore.WebUI.Controllers
{
	[Authorize]
	public class AdminController : Controller
	{
		private readonly IInstrumentRepository repository;

		public AdminController(IInstrumentRepository repo)
		{
			repository = repo;
		}

		// =========================
		// LIST
		// =========================
		public ViewResult Index()
		{
			return View(repository.Instruments);
		}

		// =========================
		// EDIT (GET)
		// =========================
		public ViewResult Edit(int id)
		{
			var Instrument = repository.Instruments
				.FirstOrDefault(p => p.Id == id);

			if (Instrument == null)
			{
				return View("Error"); // or RedirectToAction("Index")
			}

			return View(Instrument);
		}

		// =========================
		// CREATE
		// =========================
		public ViewResult Create()
		{
			return View("Edit", new Instrument());
		}

		// =========================
		// SAVE (CREATE + EDIT)
		// =========================
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Instrument Instrument, HttpPostedFileBase image)
		{
			if (!ModelState.IsValid)
			{
				return View(Instrument);
			}

			// GET existing record (important for updates)
			var existing = repository.Instruments
				.FirstOrDefault(p => p.Id == Instrument.Id);

			if (existing != null)
			{
				// UPDATE existing fields safely
				existing.Name = Instrument.Name;
				existing.Price = Instrument.Price;
				existing.Description = Instrument.Description;
				existing.Category = Instrument.Category;

				// IMAGE LOGIC (ONLY overwrite if new image uploaded)
				if (image != null && image.ContentLength > 0)
				{
					existing.ImageMimeType = image.ContentType;

					existing.ImageData = new byte[image.ContentLength];
					image.InputStream.Read(existing.ImageData, 0, image.ContentLength);
				}

				repository.SaveInstrument(existing);
			}
			else
			{
				// CREATE NEW
				if (image != null && image.ContentLength > 0)
				{
					Instrument.ImageMimeType = image.ContentType;

					Instrument.ImageData = new byte[image.ContentLength];
					image.InputStream.Read(Instrument.ImageData, 0, image.ContentLength);
				}

				repository.SaveInstrument(Instrument);
			}

			TempData["message"] = "Instrument saved";
			return RedirectToAction("Index");
		}

		// =========================
		// DELETE
		// =========================
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id)
		{
			var Instrument = repository.Instruments
				.FirstOrDefault(p => p.Id == id);

			if (Instrument != null)
			{
				repository.DeleteInstrument(id);
				TempData["message"] = "Instrument deleted";
			}

			return RedirectToAction("Index");
		}
	}
}