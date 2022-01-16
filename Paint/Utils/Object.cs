using Newtonsoft.Json;

namespace Paint.Utils
{
    public static class Object
    {
        public static T? DeepClone<T>(T o)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(o));
        }
    }
}