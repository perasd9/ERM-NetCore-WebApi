using BusinessLogic;
using BusinessLogic.Interfaces;
using BusinessLogic.Validation;
using DataAccess.EntityFramework;
using DataAccess.EntityFramework.KabinetRepo;
using DataAccess.EntityFramework.OpremaRepo;
using DataAccess.EntityFramework.TipOpremeRepo;
using DataAccess.EntityFramework.UnitOfWork;
using DataAccess.EntityFramework.ZaduzivanjeRepo;
using DataAccess.EntityFramework.ZaposleniRepo;
using Microsoft.EntityFrameworkCore;

namespace EvidentiranjeOpremeZaFakultet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                });
            });

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContextFactory<OpremaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddTransient<IZaposleniRepository, ZaposleniRepository>();
            builder.Services.AddTransient<IOpremaRepository, OpremaRepository>();
            builder.Services.AddTransient<ITipOpremeRepository, TipOpremeRepository>();
            builder.Services.AddTransient<IKabinetRepository, KabinetRepository>();
            builder.Services.AddTransient<IZaduzivanjeRepository, ZaduzivanjeRepository>();

            builder.Services.AddTransient<OpremaValidator>();
            builder.Services.AddTransient<ZaduzivanjeValidator>();

            builder.Services.AddTransient<IKabinetLogic, KabinetLogic>();
            builder.Services.AddTransient<IZaposleniLogic, ZaposleniLogic>();
            builder.Services.AddTransient<IOpremaLogic, OpremaLogic>();
            builder.Services.AddTransient<ITipOpremeLogic, TipOpremeLogic>();
            builder.Services.AddTransient<IZaduzivanjeLogic, ZaduzivanjeLogic>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(options =>
            {
                options.AllowAnyOrigin();
                options.AllowAnyMethod();
                options.AllowAnyHeader();

            });

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

        }
    }
}