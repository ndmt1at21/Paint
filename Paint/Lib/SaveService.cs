using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Lib
{
    public class SaveService<T>
    {
        public Action OnSave;
        public Action OnSaved;
        public Func<T> OnBeforeSave;

        protected bool _isSaving { get; set; }
        protected IPersister<T> _persister { get; set; }

        public SaveService(IPersister<T> persister)
        {
            _persister = persister;
        }

        public void Save(T data, string path)
        {
            OnSave?.Invoke();
            _isSaving = true;

            _persister.Save(path, data);

            _isSaving = false;
            OnSaved?.Invoke();
        }

        public Task SaveAsync(T data, string path)
        {
            return Task.Run(async () =>
            {
                if (_isSaving)
                    return;

                _isSaving = true;
                OnSave?.Invoke();

                await _persister.SaveAsync(path, data);

                OnSaved?.Invoke();
                _isSaving = false;
            });
        }
    }
}
