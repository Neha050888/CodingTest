using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodingTest.Entities.Interface
{
    public interface IRepository<T> where T : class
    {
        public Task<T> SaveToFile(T obj, string file);
    }
}
