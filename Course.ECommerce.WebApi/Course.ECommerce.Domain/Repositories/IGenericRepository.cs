﻿using Course.ECommerce.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ECommerce.Domain.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Listar todos los objetos de una entidad que se declara al niicial el repositorio
        /// </summary>
        /// <returns></returns>
        Task<ICollection<T>> GetAllAsync();

        /// <summary>
        /// Obtener un objeto por su clave primaria
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(string id);

        /// <summary>
        /// Sobrecarga para que obtener por id me reciba un tipo Guid como parametro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(Guid Id);

        /// <summary>
        /// Creacion de un nuevo producto
        /// </summary>
        /// <returns></returns>
        Task<T> InsertAsync(T entity);

        /// <summary>
        /// Modificacion de un  producto
        /// </summary>
        /// <returns></returns>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Eliminar producto
        /// </summary>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// Eliminar producto
        /// </summary>
        /// <returns></returns>
        Task<bool> DeleteAsync(string id);

        /// <summary>
        /// Retorna la consulta cuando la requiero
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetQueryable();

    }
}
