using magicPlace_webApi.Models;

namespace magicPlace_webApi.Repository.IRepository
{
    public interface IRoomRepository: IGenericRepository<Room>
    {


        //aqui soilo vamos a tener el metodo actualizar 
        Task<Room> Update(Room room);
    }
}
