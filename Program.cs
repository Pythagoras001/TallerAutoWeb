// Program.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

using BibTaller.Clases;
using BibTaller.Aspectos;
using Castle.DynamicProxy;

var builder = WebApplication.CreateBuilder(args);

// -------------------------------------------------------
// Servicios básicos
// -------------------------------------------------------
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<ProxyGenerator>();
builder.Services.AddScoped<Interceptor_RegistroDB>(); // lo registramos

// -------------------------------------------------------
// Construimos el ServiceProvider temporalmente
// para poder crear el Taller con sus dependencias
// -------------------------------------------------------
var env = builder.Environment;
var tempProvider = builder.Services.BuildServiceProvider();

// obtenemos el IHttpContextAccessor para pasarlo al interceptor
var httpContextAccessor = tempProvider.GetRequiredService<IHttpContextAccessor>();

// creamos el interceptor
var interceptor = new Interceptor_RegistroDB(httpContextAccessor);

// rutas de los archivos JSON
var rutaUsuarios = Path.Combine(env.ContentRootPath, "BaseDatos", "Usuarios.json");
var rutaOrdenes = Path.Combine(env.ContentRootPath, "BaseDatos", "Ordenes.json");
var rutaFacturas = Path.Combine(env.ContentRootPath, "BaseDatos", "Facturas.json");

// instancia de Taller (única para toda la app)
var taller = new Taller(interceptor, rutaUsuarios, rutaOrdenes, rutaFacturas);

// registramos la instancia como Singleton
builder.Services.AddSingleton(taller);

// -------------------------------------------------------
// Pipeline MVC
// -------------------------------------------------------
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
