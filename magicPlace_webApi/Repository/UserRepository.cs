using magicPlace_webApi.DataStore;
using magicPlace_webApi.Models;
using magicPlace_webApi.Models.Dto;
using magicPlace_webApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Text;

namespace magicPlace_webApi.Repository
{
    public class UserRepository : IUserRepository
    {

        //crearemos su constructor por que vams a tener que inyexctar Nustro Dbcontext 

        private readonly ApplicationDbContext _db;

        private string _secretKey;
        public UserRepository(ApplicationDbContext db, IConfiguration configuration)
        {

            _db = db;
            _secretKey = configuration.GetValue<string>("ApiSettings:Secret");

        }

       

        //metodo mejorado  
        public async Task<bool> IsUserUnique(string username)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == username.ToLower());

            if (user == null)
            {
                return true;
            }

            return false;
        }


        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {

            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower() &&
                                                                u.UserPassword == loginRequestDto.Password);
            if (user == null)
            {

                return new LoginResponseDto()
                {

                    Token = "",
                    User = null

                };
            }

            //Create Token 

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new Claim[] {


                    new Claim(ClaimTypes.Name,user.Id.ToString()),
                    new Claim(ClaimTypes.Role,user.UserRol)

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

            };


            var token = tokenHandler.CreateToken(tokenDescriptor);


            LoginResponseDto loginResponseDto = new()
            {


                Token = tokenHandler.WriteToken(token),
                User = user,
            };

            return loginResponseDto;


        }

        public async Task<User> Register(RegisterRequestDto registerRequestDto)
        {
            User user = new()
            {
                UserName = registerRequestDto.UserName,
                UserEmail = registerRequestDto.UserEmail,
                UserPassword = registerRequestDto.UserPassword,
                UserRol = registerRequestDto.UserRol,

            };

            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            //antes de devolverlo vamos a borra el password
            user.UserPassword = "";
            return user;
        }
    }
}
