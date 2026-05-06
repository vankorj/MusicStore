using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStore.Domain.Models;
using System.Data.Entity;

namespace MusicStore.Domain.Concrete
{
	public class EFDbContext : DbContext
	{
		public DbSet<Instrument> Instruments { get; set; }
	}
}
