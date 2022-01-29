using Newtonsoft.Json;

namespace Paint.Utils
{
    public static class Object
    {
        private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented,
        };

        public static T? DeepClone<T>(T o)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(o, _settings), _settings);
        }
    }
}