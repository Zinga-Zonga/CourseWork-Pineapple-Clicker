using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Data
{
    internal interface IRepository<T>
    {
       
        List<T> GetAll();
        void Add(T entity);
        void Remove(int id);
        T GetById(int id);
        List<T> GetList();
    }
}
