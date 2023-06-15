using MagicPlaceFront.Models.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicPlaceFront.Models.ViewModel
{
    public class OcuppanDeleteViewModel
    {

        public OccupantDto ObjOccupant { get; set; }

        public OcuppanDeleteViewModel()
        {
            ObjOccupant = new OccupantDto();
        }

        public IEnumerable<SelectListItem> RoomList { get; set; }
    }
}
