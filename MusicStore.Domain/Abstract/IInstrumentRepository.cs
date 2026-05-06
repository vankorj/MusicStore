using System.Collections.Generic;
using MusicStore.Domain.Models;

namespace MusicStore.Domain.Abstract
{
	public interface IInstrumentRepository
	{
		IEnumerable<Instrument> Instruments { get; }

		void SaveInstrument(Instrument Instrument);

		Instrument DeleteInstrument(int id);
	}
}