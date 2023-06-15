using magicPlace_webApi;
using magicPlace_webApi.DataStore;
using magicPlace_webApi.Repository;
using magicPlace_webApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//agregamos el servicio de la base de datos 

builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));

//servicio de automapper

builder.Services.AddAutoMapper(typeof(MappingConfig));


//servicio de repositorio creado por nosotros 

builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IOccupantRepository, OccupantRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
