using Condominios.Data;
using Condominios.Data.Interfaces;
using Condominios.Data.Interfaces.IRepositories;
using Condominios.Data.Repositories.Catalogos;
using Condominios.Data.Repositories.CtrlEquipos;
using Condominios.Data.Repositories.Equipos;
using Condominios.Data.Repositories.Mantenimientos;
using Condominios.Models;
using Condominios.Models.DTOs;
using Condominios.Models.Entities;
using Condominios.Models.Services;
using Condominios.Models.Services.Classes;
using Condominios.Models.Services.Classes.SegundoPlano;
using Condominios.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Settings for Mexico Spanish - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
var culture = new CultureInfo("es-MX");
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

// Tareas en segundo plano - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
builder.Services.AddHostedService<WorkerAutomaticMtos>();
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

// Add services Scoped - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
// Servicios específicos
builder.Services.AddScoped<IEpoch, EpochService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAutenticarService, AutenticarService>();

// Servicios de Catálogos
builder.Services.AddScoped<ICatalogoRepository<Marca>, MarcaRepository>();
builder.Services.AddScoped<ICatalogoRepository<Motor>, MotorRepository>();
builder.Services.AddScoped<ICatalogoRepository<Periodo>, PeriodoRepository>();
builder.Services.AddScoped<ICatalogoRepository<Ubicacion>, UbicacionRepository>();
builder.Services.AddScoped<ICatalogoRepository<Estatus>, EstatusRepository>();
builder.Services.AddScoped<ICatalogoRepository<TipoEquipo>, TipoEquipoRepository>();
builder.Services.AddScoped<ICatalogoRepository<TipoMantenimiento>, TipoMtoRepository>();
builder.Services.AddScoped<ICatalogoRepository<UnidadMedida>, UnidadMedidaRepository>();
 
// Servicios para el control de equipos
builder.Services.AddScoped<IEquipoRepository<Equipo>, EquipoRepository>();
builder.Services.AddScoped<IVarianteRepository<VarianteDTO>, VarianteRepository>();
builder.Services.AddScoped<IProveedorRepository<Proveedor>, ProveedorRepository>();
builder.Services.AddScoped<IMtoRepository, MtoRepository>();

// Servicios intermedios entre repositorio y Unidad de trabajo
builder.Services.AddScoped<AutenticarService>();
builder.Services.AddScoped<CatalogoService>();
builder.Services.AddScoped<CtrlEquipoService>();
builder.Services.AddScoped<VarianteService>();
builder.Services.AddScoped<ProveedorService>();
builder.Services.AddScoped<MtoService>();

// Servicios de Perfil
builder.Services.AddScoped<IPerfilRepository<Usuario>, PerfilRepository>();         
builder.Services.AddScoped<PerfilService>();

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
// Configuraciones de autenticación por roles - - - - - - - - - - - - - - - - - - - - - - - 
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Acceso/Login";
        //option.ExpireTimeSpan = TimeSpan.FromSeconds(10);
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        option.AccessDeniedPath = "/Home/Privacy";
    });

// Inject DataBase - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Context"));
});
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

var app = builder.Build();

// Inicialización de datos - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
using (var serviceScope = app.Services.CreateScope())
{
    var serviceProvider = serviceScope.ServiceProvider;
    var context = serviceProvider.GetRequiredService<Context>();

    Dependencias datos = new(context);
    datos.AgregarCatalogos().Wait();
}
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
    // Manejador de exepciones
    app.UseExceptionHandler("/Home/Error");
    // Habilita la politica de seguridad de HTTPS, el uso de HSTP (HTTP Strict Transport Security)
    app.UseHsts();
}
else if (app.Environment.IsDevelopment()) {  }

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Configuraciones de autenticación por roles - - - - - - - - - - - - - - - - - - - - - 
app.UseAuthentication();
app.UseAuthorization();
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=Login}/{id?}");

app.Run();