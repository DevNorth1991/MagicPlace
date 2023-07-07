using magicPlace_webApi;
using magicPlace_webApi.DataStore;
using magicPlace_webApi.Repository;
using magicPlace_webApi.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => {

    options.CacheProfiles.Add("Default30", new CacheProfile() { 
    
    
            Duration = 30
    
    
    });
    options.CacheProfiles.Add("Default15", new CacheProfile()
    {


        Duration = 15


    });


}).AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


// docuentacion de la autenticacion en Swagger  

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Ingresar Bearer [space] tuToken \r\n\r\n " +
                      "Ejemplo: Bearer 123456abcder",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id= "Bearer"
                },
                Scheme = "oauth2",
                Name="Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
    options.SwaggerDoc("v1",new OpenApiInfo { 
    
            Version= "v1",
            Title="Magic Place V1",
            Description="Api rest Service for Hotels"

    });
    options.SwaggerDoc("v2", new OpenApiInfo
    {

        Version = "v2",
        Title = "Magic Place V2",
        Description = "Api rest Service for Hotels"

    });
});
//desde aqui inyectaremos el servicio de autenticacion de swagger en nuestra api 
var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });



//agregamos el servicio de la base de datos 

builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));

//servicio de automapper

builder.Services.AddAutoMapper(typeof(MappingConfig));


//servicio de repositorio creado por nosotros 

builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IOccupantRepository, OccupantRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//adding versioning service

builder.Services.AddApiVersioning(options => { 

    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;

});

builder.Services.AddVersionedApiExplorer(options => {


    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl= true;


});

//adding caching services

builder.Services.AddResponseCaching(options =>
{



});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(option => {
        //el metodo SwaggerEndpoint("ruta de swagger por defecto", "Nombre que le quisieramos poner ");
        option.SwaggerEndpoint("/swagger/v1/swagger.json", "Magic_Place_v1");
        option.SwaggerEndpoint("/swagger/v2/swagger.json", "Magic_Place_v2");

    });
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
