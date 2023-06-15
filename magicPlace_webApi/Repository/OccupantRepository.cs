using magicPlace_webApi.DataStore;
using magicPlace_webApi.Models;
using magicPlace_webApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace magicPlace_webApi.Repository
{
    public class OccupantRepository : Repository<Occupant>, IOccupantRepository
    {

        private readonly ApplicationDbContext _context;

        public OccupantRepository(ApplicationDbContext context) : base(context)
        {

            _context = context;

        }


        public async Task<Occupant> Update(Occupant occupa)
        {

            occupa.DateUpdateOcccupant = DateTime.Now;
            _context.Update(occupa);
            await _context.SaveChangesAsync();
            return occupa;


        }
    }
}
