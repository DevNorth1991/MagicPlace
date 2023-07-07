using MagicPlaceFront.Models.Dto;

namespace MagicPlaceFront.Models.ViewModel
{
    public class RoomPaginatedViewModel
    {


        public int  PageNumber { get; set; }

        public int TotalPages { get; set; }

        public string Previous { get; set; } = "disabled";

        public string Next { get; set; } = "";

        public IEnumerable<RoomDto> RoomList { get; set; }
        

    }
}
