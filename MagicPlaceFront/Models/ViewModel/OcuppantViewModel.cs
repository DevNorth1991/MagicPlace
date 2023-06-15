using MagicPlaceFront.Models.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicPlaceFront.Models.ViewModel
{
    public class OcuppantViewModel
    {

        public OccupantCreateDto ObjOccupant { get; set; }

        public OcuppantViewModel()
        {
            ObjOccupant = new OccupantCreateDto();

        }

        public IEnumerable<SelectListItem> RoomList { get; set; }
    }
}
