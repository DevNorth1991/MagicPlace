using System.Net;

namespace MagicPlaceFront.Models
{
    public class ApiResponse
    {

        public HttpStatusCode statusCode { get; set; }

        public bool isSucces { get; set; } = true;

        public List<string> ErrorMessages { get; set; }

        //y aqui vamos a crear una varible objeto que nos da la flexibilidad de almacenar cualquir
        //cosa dentro de ella ya sea una lista un objeto etc

        public object Results { get; set; }


    }
}
