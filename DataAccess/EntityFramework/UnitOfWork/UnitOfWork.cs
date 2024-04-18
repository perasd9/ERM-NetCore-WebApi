using DataAccess.EntityFramework.KabinetRepo;
using DataAccess.EntityFramework.OpremaRepo;
using DataAccess.EntityFramework.TipOpremeRepo;
using DataAccess.EntityFramework.ZaduzivanjeRepo;
using DataAccess.EntityFramework.ZaposleniRepo;

namespace DataAccess.EntityFramework.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        //napravljen unit of work da bi se enkapsulirali repozitorijumi i obezbedila
        //mogucnost rada pod transakcijom
        private readonly OpremaContext context;
        public IZaposleniRepository ZaposleniRepository { get; set; }
        public IKabinetRepository KabinetRepository { get; set; }
        public IOpremaRepository OpremaRepository { get; set; }
        public ITipOpremeRepository TipOpremeRepository { get; set; }
        public IZaduzivanjeRepository ZaduzivanjeRepository { get; set; }

        public UnitOfWork(OpremaContext context)
        {
            this.context = context;
            ZaposleniRepository = new ZaposleniRepository(context);
            KabinetRepository = new KabinetRepository(context);
            OpremaRepository = new OpremaRepository(context);
            TipOpremeRepository = new TipOpremeRepository(context);
            ZaduzivanjeRepository = new ZaduzivanjeRepository(context);
        }
        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}
