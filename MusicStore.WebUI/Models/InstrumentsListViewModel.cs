using System.Collections.Generic;
using MusicStore.Domain.Models;

namespace MusicStore.WebUI.Models
{
	public class InstrumentsListViewModel
	{
		public IEnumerable<Instrument> Instruments { get; set; }

		public PagingInfo PagingInfo { get; set; }

		public string CurrentCategory { get; set; }
	}
}