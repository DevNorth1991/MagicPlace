﻿namespace magicPlace_webApi.Models.Dto
{
    public class LoginResponseDto
    {

        public UserDto User { get; set; }
        public string Token { get; set; }
        public string Rol { get; set; }


    }
}
