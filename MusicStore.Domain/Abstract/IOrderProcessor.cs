using MusicStore.Domain.Models;
using MusicStore.WebUI.Models;

namespace MusicStore.Domain.Abstract
{
	public interface IOrderProcessor
	{
		void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
	}
}
