using MusicStore.Domain.Abstract;
using MusicStore.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace MusicStore.Domain.Concrete
{
	public class EFInstrumentRepository : IInstrumentRepository
	{
		private EFDbContext context = new EFDbContext();

		public IEnumerable<Instrument> Instruments => context.Instruments;

		public void SaveInstrument(Instrument Instrument)
		{
			if (Instrument.Id == 0)
			{
				context.Instruments.Add(Instrument);
			}
			else
			{
				Instrument dbEntry = context.Instruments.Find(Instrument.Id);

				if (dbEntry != null)
				{
					dbEntry.Name = Instrument.Name;
					dbEntry.Description = Instrument.Description;
					dbEntry.Price = Instrument.Price;
					dbEntry.Category = Instrument.Category;

					// =========================
					// IMAGE SUPPORT (NEW)
					// =========================
					if (Instrument.ImageData != null)
					{
						dbEntry.ImageData = Instrument.ImageData;
						dbEntry.ImageMimeType = Instrument.ImageMimeType;
					}
				}
			}

			context.SaveChanges();
		}

		public Instrument DeleteInstrument(int id)
		{
			Instrument dbEntry = context.Instruments.Find(id);

			if (dbEntry != null)
			{
				context.Instruments.Remove(dbEntry);
				context.SaveChanges();
			}

			return dbEntry;
		}
	}
}