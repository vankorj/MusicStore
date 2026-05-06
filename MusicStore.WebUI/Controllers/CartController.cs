using MusicStore.Domain.Abstract;
using MusicStore.Domain.Models;
using MusicStore.WebUI.Models;
using System.Linq;
using System.Web.Mvc;

namespace MusicStore.WebUI.Controllers
{
	public class CartController : Controller
	{
		private IInstrumentRepository repository;
		private IOrderProcessor orderProcessor;

		public CartController(IInstrumentRepository repo, IOrderProcessor processor)
		{
			repository = repo;
			orderProcessor = processor;
		}

		public RedirectToRouteResult AddToCart(Cart cart, int id, string returnUrl)
		{
			Instrument Instrument = repository.Instruments
				.FirstOrDefault(g => g.Id == id);

			if (Instrument != null)
			{
				cart.AddItem(Instrument, 1);
			}

			return RedirectToAction("Index", new { returnUrl });
		}

		public RedirectToRouteResult RemoveFromCart(Cart cart, int id, string returnUrl)
		{
			Instrument Instrument = repository.Instruments
				.FirstOrDefault(g => g.Id == id);

			if (Instrument != null)
			{
				cart.RemoveLine(Instrument);
			}

			return RedirectToAction("Index", new { returnUrl });
		}

		public ViewResult Index(Cart cart, string returnUrl)
		{
			return View(new CartIndexViewModel
			{
				Cart = cart,
				ReturnUrl = returnUrl
			});
		}

		public PartialViewResult Summary(Cart cart)
		{
			return PartialView(cart);
		}

		public ViewResult Checkout()
		{
			return View(new ShippingDetails());
		}

		[HttpPost]
		public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
		{
			if (!cart.Lines.Any())
			{
				ModelState.AddModelError("", "Sorry, your cart is empty!");
			}

			if (ModelState.IsValid)
			{
				orderProcessor.ProcessOrder(cart, shippingDetails);

				cart.Clear();

				return View("Completed", shippingDetails);
			}

			return View(shippingDetails);
		}
	}
}