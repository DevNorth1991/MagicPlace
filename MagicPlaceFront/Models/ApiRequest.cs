﻿using Microsoft.AspNetCore.Mvc;
using static MagicPlace_Utilities.SD;

namespace MagicPlaceFront.Models
{
    public class ApiRequest
    {

        //aqui usaremos la clase Utilities de nuestra libreria de clases 

        public ApiType ApiTypes { get; set; } = ApiType.GET;
        public string ApiUrl { get; set; }
        public object Data { get; set; }
        public string Token { get; set; }
        public Parameters parameters { get; set; } 
        
    }



    public class Parameters
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }


}
