using Microsoft.EntityFrameworkCore;
using SistemaInventario.AccesoDatos.Repositorios.IRepositorio;
using sistemaInventariov6.AccesoDatos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorios
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        //creamos esta clase que por herencia pasamos IRepositorio

        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet; //esto nos ayudara para la manipulacon de datos de la bd

        public Repositorio(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public void Agregar(T entidad)
        {
            dbSet.Add(entidad); // insert into table
        }

        public T Obtener(int id)
        {
            return dbSet.Find(id); //select * from
        }

        public T ObtenerPrimero(Expression<Func<T, bool>> filer = null, string incluirPropiedades = null)
        {
            IQueryable<T> query = dbSet;
            //primer parametro es filter
            if (filer != null)
            {
                query = query.Where(filer);//select * from where
            }
            //validamos propiedades (si se incluyen o no)
            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp);
                }
            }

            return query.FirstOrDefault();
        }

        public IEnumerable<T> ObtenerTodos(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string incluirPropiedades = null)
        {
            IQueryable<T> query = dbSet;
            //primer parametro es filter
            if (filter != null)
            {
                query = query.Where(filter);//select * from where
            }
            //validamos propiedades (si se incluyen o no)
            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp);
                }
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        public void Remover(int id)
        {
            T entidad = dbSet.Find(id);
            Remover(entidad);
        }

        public void Remover(T entidad)
        {
            dbSet.Remove(entidad); //delete from
        }

        public void RemoverRango(IEnumerable<T> entidad)
        {
            dbSet.RemoveRange(entidad);
        }
    }
}