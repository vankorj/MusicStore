using MusicStore.Domain.Models;
using MusicStore.WebUI.Models;
using System.Collections.Generic;

namespace MusicStore.WebUI.Models
{
	public class InstrumentsListViewModel
	{
		public IEnumerable<Instrument> Instruments { get; set; }
		public PagingInfo PagingInfo { get; set; }

		public string CurrentCategory { get; set; }
		public string SearchTerm { get; set; }
		public string CurrentSort { get; set; }
	}
}