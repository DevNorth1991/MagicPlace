using magicPlace_webApi.Models.Specifications;
using System.Linq.Expressions;

namespace magicPlace_webApi.Repository.IRepository
{
    public interface IGenericRepository<T> where T : class
    {



        Task Create(T entidad);



        //vamos a crear el metodo Obtener todos pero que puede o no recibir una expresion linq como filtro
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filtro = null,string? incluir=null);



        //vamos a crear el metodo Obtener todos pero que puede o no recibir una expresion linq como filtro pero 
        //que a su vez va a implementar el paginado
        PagedList<T> GetAllPaginated(Parameters parametros ,Expression<Func<T, bool>>? filtro = null, string? incluir = null);


        //get by id ,,. recordemos que este metodo tambien va  ahacer uso del hasNoTraking asi que vamos a ponerselo como
        //parametro del metodo
        Task<T> getById(Expression<Func<T, bool>>? filtro = null, bool tracked = true,string? incluir= null);



        //metodo remover 

        Task Delete(T entidad);



        //metodo que se va a encargar de manejar el save Changes del dbContext
        Task Save();






    }

}
