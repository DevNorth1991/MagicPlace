using magicPlace_webApi.DataStore;
using magicPlace_webApi.Models;
using magicPlace_webApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace magicPlace_webApi.Repository
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {

        private readonly ApplicationDbContext _context;

        public RoomRepository(ApplicationDbContext context) : base(context)
        {

            _context = context;

        }


        public async Task<Room> Update(Room room)
        {

            room.UpdateTime = DateTime.Now;
            _context.Update(room);
            await _context.SaveChangesAsync();
            return room;


        }
    }
}
