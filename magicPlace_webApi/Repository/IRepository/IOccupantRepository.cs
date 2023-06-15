using magicPlace_webApi.Models;

namespace magicPlace_webApi.Repository.IRepository
{
    public interface IOccupantRepository: IGenericRepository<Occupant>
    {


        //aqui soilo vamos a tener el metodo actualizar 
        Task<Occupant> Update(Occupant occupa);
    }
}
