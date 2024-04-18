using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework
{
    public class OpremaContext : DbContext
    {
        public OpremaContext(DbContextOptions<OpremaContext> options) : base(options)
        {

        }

        public DbSet<Kabinet> Kabineti { get; set; }
        public DbSet<Oprema> Oprema { get; set; }
        public DbSet<TipOpreme> TipOpreme { get; set; }
        public DbSet<Zaduzivanje> Zaduzivanje { get; set; }
        public DbSet<Zaposleni> Zaposleni { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Zaposleni>().HasKey(z => z.Email);
            modelBuilder.Entity<Zaposleni>().HasOne(z => z.Kabinet).WithMany(k => k.ListaZaposlenih);

            modelBuilder.Entity<Oprema>().HasKey(o => o.SerijskiBroj);
            modelBuilder.Entity<Oprema>().HasOne(o => o.TipOpreme).WithMany(to => to.ListaOpreme);

            modelBuilder.Entity<TipOpreme>().HasKey(to => to.TipOpremeId);
            modelBuilder.Entity<TipOpreme>().HasOne(to => to.Nadtip).WithMany(to => to.ListaPodTipova).HasForeignKey(to => to.NadtipId);

            modelBuilder.Entity<Kabinet>().HasKey(k => k.KabinetId);
            modelBuilder.Entity<Kabinet>().HasMany(k => k.ListaZaduzivanja).WithOne(zad => zad.Kabinet);

            modelBuilder.Entity<Zaduzivanje>().HasKey(zad => new
            {
                zad.Email,
                zad.SerijskiBroj,
                zad.DatumZaduzivanja,
                zad.ZaduzivanjeId
            });
            modelBuilder.Entity<Zaduzivanje>().HasOne(zad => zad.Zaposleni).WithMany(z => z.ListaZaduzivanja).HasForeignKey(zad => zad.Email);
            modelBuilder.Entity<Zaduzivanje>().HasOne(zad => zad.Oprema).WithMany(o => o.ListaZaduzivanja).HasForeignKey(zad => zad.SerijskiBroj);


            modelBuilder.Entity<Oprema>(x =>
            {
                x.Property(y => y.RowVersion).IsRowVersion();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
