using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Paint.Lib
{
    public class JsonPersister<T> : IPersister<T>
    {
        private readonly JsonSerializerSettings _serializeSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public JsonPersister()
        {
        }

        public T Load(string path)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path), _serializeSettings);
        }

        public void Save(string path, T data)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));

            string content = JsonConvert.SerializeObject(data, _serializeSettings);
            File.WriteAllText(path, content);
        }

        public Task SaveAsync(string path, T data)
        {
            return Task.Run(async () =>
            {
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                    Directory.CreateDirectory(Path.GetDirectoryName(path));

                string content = JsonConvert.SerializeObject(data, _serializeSettings);
                await File.WriteAllTextAsync(path, content);
            });
        }

        public void Delete(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
