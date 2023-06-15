using MagicPlaceFront.Models.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicPlaceFront.Models.ViewModel
{
    public class OcuppantUpdateViewModel
    {

        public OccupantUpdateDto ObjOccupant { get; set; }

        public OcuppantUpdateViewModel()
        {
            ObjOccupant = new OccupantUpdateDto();

        }

        public IEnumerable<SelectListItem> RoomList { get; set; }
    }
}
