using NetCoreLinqToSqlInjection.Models;
using NetCoreLinqToSqlInjection.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//INYECTAMOS NUESTRA INTERFACE Y LA CLASE
builder.Services.AddTransient<IRepositoryDoctores, RepositoryDoctoresOracle>();

//RESOLVEMOS EL SERVICIO COCHE
//builder.Services.AddTransient<Coche>();
//builder.Services.AddSingleton<Coche>();
//builder.Services.AddSingleton<Deportivo>();

//Por último, al resolver el servicio debemos indicar el Molde (interface) y la clase que enviamos 
//Con dicho Molde.
//builder.Services.AddSingleton<Interface, Clase>();
//builder.Services.AddSingleton<ICoche, Deportivo>();

//El último concepto es poder crear objetos dentro del Container y enviarlos desde ahí.
Coche car = new Coche();
car.Marca = "PONTIAC";
car.Modelo = "RAYO";
car.Imagen = "coche3.jpg";
car.Velocidad = 0;
car.VelocidadMaxima = 280;
//PARA ENVIAR NUESTRO OBJETO PERSONALIZADO -> LAMBDA
builder.Services.AddSingleton<ICoche, Coche>(x => car);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
