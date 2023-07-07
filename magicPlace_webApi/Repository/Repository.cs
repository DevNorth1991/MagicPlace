using magicPlace_webApi.DataStore;
using magicPlace_webApi.Models.Specifications;
using magicPlace_webApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace magicPlace_webApi.Repository
{
    public class Repository<T> : IGenericRepository<T> where T : class
    {


        private readonly ApplicationDbContext _context;

        //aparte tambien necesitariamos una variable del tipo DbSet que es quien va a hacer
        //la conversion del tipo T que estamos 
        //recibiendo a una entidad de nuestra base de datos 

        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext context)
        {

            _context = context;

            //ahora veamos como conveertimos el generico T en una entidad 

            this.dbSet = _context.Set<T>();
        }





        public async Task Create(T entidad)
        {
            await dbSet.AddAsync(entidad);

            await Save();
        }



        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filtro = null, string? incluir = null)
        {


            IQueryable<T> query = dbSet;

            if (filtro != null)
            {

                query = query.Where(filtro);
            }

            if (incluir != null)
            {//"Room,OtroModelo"

                //vamos  a separar y limpiar los prametros por medio de un foreach

                foreach (var includeProps in incluir.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProps);

                }


            }

            return await query.ToListAsync();

        }



        //get All Paginated 

        public PagedList<T> GetAllPaginated(Parameters parametros, Expression<Func<T, bool>>? filtro = null, string? incluir = null)
        {


            IQueryable<T> query = dbSet;

            if (filtro != null)
            {

                query = query.Where(filtro);
            }

            if (incluir != null)
            {//"Room,OtroModelo"

                //vamos  a separar y limpiar los prametros por medio de un foreach

                foreach (var includeProps in incluir.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProps);

                }


            }

            return PagedList<T>.ToPagedList(query, parametros.PageNumber, parametros.PageSize);

        }



        //traer por id 

        public async Task<T> getById(Expression<Func<T, bool>>? filtro = null, bool tracked = true, string? incluir = null)
        {

            //primero vamos a necesitar una variable del tipo IQueryable

            IQueryable<T> query = dbSet;

            if (!tracked)
            {

                query = query.AsNoTracking();

            }

            if (filtro != null)
            {//nos estan enviando una expresion linq 

                query = query.Where(filtro); //enviamos una expresion linq a la query 
            }

            if (incluir != null)
            {

                foreach (var includeProps in incluir.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProps);

                }


            }

            return await query.FirstOrDefaultAsync();


        }



        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }



        public async Task Delete(T entidad)
        {
            dbSet.Remove(entidad);
            await Save();
        }

       
    }
}
