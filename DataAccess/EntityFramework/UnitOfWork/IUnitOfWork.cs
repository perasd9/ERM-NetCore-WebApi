using DataAccess.EntityFramework.KabinetRepo;
using DataAccess.EntityFramework.OpremaRepo;
using DataAccess.EntityFramework.TipOpremeRepo;
using DataAccess.EntityFramework.ZaduzivanjeRepo;
using DataAccess.EntityFramework.ZaposleniRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IZaposleniRepository ZaposleniRepository { get; set; }
        public IKabinetRepository KabinetRepository { get; set; }
        public IOpremaRepository OpremaRepository { get; set; }
        public ITipOpremeRepository TipOpremeRepository { get; set; }
        public IZaduzivanjeRepository ZaduzivanjeRepository { get; set; }

        public Task SaveChanges();
    }
}
