using AutoMapper;
using magicPlace_webApi.DataStore;
using magicPlace_webApi.Models;
using magicPlace_webApi.Models.Dto;
using magicPlace_webApi.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Text;

namespace magicPlace_webApi.Repository
{
    public class UserRepository : IUserRepository
    {

        //crearemos su constructor por que vams a tener que inyexctar Nustro Dbcontext 

        private readonly ApplicationDbContext _db;

        //para acceder a trabajar con los ususrios mediante Identity necesitamos inyectar UserManager 
        //y enviarle comom parametro el modelo de usuario que hereda de Identity 

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        //vamos a inyectar mapper
        private readonly IMapper _mapper;

        private string _secretKey;

        public UserRepository(ApplicationDbContext db,
                                IConfiguration configuration,
                                UserManager<ApplicationUser> userManager,
                                IMapper mapper,
                                RoleManager<IdentityRole> roleManager)
        {

            _db = db;
            _secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }



        //metodo mejorado  
        public async Task<bool> IsUserUnique(string username)
        {
            var user = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName.ToLower() == username.ToLower());

            if (user == null)
            {
                return true;
            }

            return false;
        }


        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _db.ApplicationUsers.SingleOrDefaultAsync(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user == null || isValid == false)
            {

                return new LoginResponseDto()
                {

                    Token = "",
                    User = null

                };
            }

            //si el usuario existe verificamos el rol del mismo tebems que tener en cuenta que ahora los roles se guardan en 
            //una tabla diferente 

            var roles = await _userManager.GetRolesAsync(user);

            //Create Token 
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new Claim[] {


                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(ClaimTypes.Role,roles.FirstOrDefault())

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

            };


            var token = tokenHandler.CreateToken(tokenDescriptor);


            LoginResponseDto loginResponseDto = new()
            {
                Token = tokenHandler.WriteToken(token),
                User = _mapper.Map<UserDto>(user),
            };

            return loginResponseDto;


        }

        public async Task<UserDto> Register(RegisterRequestDto registerRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registerRequestDto.UserName,
                NameUserIdentity = registerRequestDto.UserName,
                Email = registerRequestDto.UserEmail,
                NormalizedEmail = registerRequestDto.UserEmail.ToUpper(),

            };

            //a aprtir de aca usamos a user manager 

            try
            {

                var createNewUser = await _userManager.CreateAsync(user, registerRequestDto.UserPassword);

                if (createNewUser.Succeeded)
                {

                    //verificamos que el rol exista si el rol no existe lo creamos 

                    if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                    {

                        await _roleManager.CreateAsync(new IdentityRole("admin"));
                        await _roleManager.CreateAsync(new IdentityRole("cliente"));


                    }




                    //si se ha creado el usuario con exito le agregamos un rol 

                    await _userManager.AddToRoleAsync(user, "admin");
                    var userApp = await _db.ApplicationUsers.FirstOrDefaultAsync(user => user.UserName == registerRequestDto.UserName);

                    //retornmaos solo el dto que queremos para mostrar 
                    return _mapper.Map<UserDto>(userApp);

                }
                else
                {

                    return _mapper.Map<UserDto>(user);

                }


            }
            catch (Exception)
            {

                throw;
            }

            return new UserDto();


        }
    }
}
