namespace magicPlace_webApi.Models.Specifications
{
    public class PagedList<T> : List<T>
    {

        //esta es la clasae que se encargara de realizar los cortes y los metadatos 

        public MetaData metaData { get; set; }


        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {


            metaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
            };

            AddRange(items);


        }


        //metodo estatico quien devolvera el pagedListConfigurado 

        public static PagedList<T> ToPagedList(IEnumerable<T> entidad, int pageNumber, int pageSize)
        {

            var count = entidad.Count();
            var items = entidad.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }



    }
}
/*
 
La razón por la cual el método ToPagedList recibe un parámetro IEnumerable<T> en lugar de IQueryable<T> es para proporcionar flexibilidad en la implementación.

Al recibir IEnumerable<T> como parámetro, el método ToPagedList puede aceptar tanto IEnumerable<T> como IQueryable<T>. Esto se debe a que IQueryable<T> hereda de IEnumerable<T>, lo que significa que cualquier objeto IQueryable<T> también se puede tratar como IEnumerable<T>.

Al utilizar IEnumerable<T> como parámetro, el método ToPagedList no se limita a recibir únicamente consultas IQueryable<T>, sino que también puede aceptar cualquier otra secuencia de elementos que implemente IEnumerable<T>. Esto brinda mayor flexibilidad y reutilización del código, ya que el método ToPagedList no está acoplado específicamente a un tipo concreto de consulta.

Dentro de la implementación del método ToPagedList, se utiliza el parámetro entidad para obtener el número total de elementos (count) y para aplicar los operadores Skip y Take en la secuencia para realizar la paginación.

En resumen, el método ToPagedList acepta un parámetro IEnumerable<T> para ser más flexible y poder recibir tanto IEnumerable<T> como IQueryable<T>. Esto permite una mayor reutilización del código y facilita la implementación del método de paginación en diferentes contextos.
 
 */