namespace DAL.Repository
{
    using System;
    using System.Collections.Generic;

    interface IRepository<TEntity> : IDisposable
    {
        ICollection<TEntity> GetAll();
        TEntity GetById(int id);
    }
}
