using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Lib
{
    public interface IPersister<T>
    {
        void Save(string path, T data);
        T Load(string path);
        Task SaveAsync(string path, T data);
        void Delete(string path);
    }
}
