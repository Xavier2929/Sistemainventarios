using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorios.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        //interfaz para repositorio para cuando haya inclusion mayores y la escala crezca en el proyecto
        T Obtener(int id);

        IEnumerable<T> ObtenerTodos(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string incluirPropiedades = null
            );

        T ObtenerPrimero(
            Expression<Func<T, bool>> filer = null,

            string incluirPropiedades = null
            );

        void Agregar(T entidad);

        void Remover(int id);

        void RemoverRango(IEnumerable<T> entidad);

        void Remover(T entidad);
    }
}