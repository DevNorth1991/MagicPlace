using MagicPlaceFront;
using MagicPlaceFront.Services;
using MagicPlaceFront.Services.IServices;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add automppaer services

builder.Services.AddAutoMapper(typeof(MappingConfig));

//add services Room Services interfaz e implementacion 

builder.Services.AddHttpClient<IRoomServices,RoomServices>();

//teambien debems agregar AddScope

builder.Services.AddScoped<IRoomServices, RoomServices>();

//AGREGANDO SERVICIOS DE CRUD OCUPANTES 
//add services Room Services interfaz e implementacion 

builder.Services.AddHttpClient<IOccupantServices, OccupantServices>();

//teambien debems agregar AddScope

builder.Services.AddScoped<IOccupantServices, OccupantServices>();

//runtime compilation
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}





app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
