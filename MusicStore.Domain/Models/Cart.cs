using System.Collections.Generic;
using System.Linq;
using MusicStore.Domain.Models;

namespace MusicStore.WebUI.Models
{
	public class Cart
	{
		public List<CartLine> Lines { get; set; } = new List<CartLine>();

		public void AddItem(Instrument Instrument, int quantity)
		{
			var line = Lines
				.Where(g => g.Instrument.Id == Instrument.Id)
				.FirstOrDefault();

			if (line == null)
			{
				Lines.Add(new CartLine
				{
					Instrument = Instrument,
					Quantity = quantity
				});
			}
			else
			{
				line.Quantity += quantity;
			}
		}

		public void RemoveLine(Instrument Instrument)
		{
			Lines.RemoveAll(l => l.Instrument.Id == Instrument.Id);
		}

		public void Clear()
		{
			Lines.Clear();
		}

		public decimal ComputeTotalValue()
		{
			return Lines.Sum(e => e.Instrument.Price * e.Quantity);
		}

		public int TotalItems()
		{
			return Lines.Sum(e => e.Quantity);
		}
	}

	public class CartLine
	{
		public Instrument Instrument { get; set; }
		public int Quantity { get; set; }
	}
}