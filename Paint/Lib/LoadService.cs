using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Lib
{
    public class LoadService<T>
    {
        private IPersister<T> _persister { get; set; }

        public LoadService(IPersister<T> persister)
        {
            _persister = persister;
        }

        public T Load(string path)
        {
            try
            {
                T data = _persister.Load(path);
                return data;
            }
            catch
            {
                throw new InvalidCastException("Invalid .bare file structure");
            }
        }
    }
}
