using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Paint.Utils
{
    public class Dll
    {
        public static FileInfo[] GetDllFilesFromFolder(string folderPath)
        {
            DirectoryInfo d = new DirectoryInfo(@folderPath);
            FileInfo[] files = d.GetFiles("*.dll", SearchOption.AllDirectories);

            return files;
        }

        public static object CreateInstanceFromDllFile(string dllPath, Type targetType, object[] argsForInstance = null)
        {
            Assembly _Assembly = Assembly.LoadFile(dllPath);
            List<Type> types = _Assembly.GetTypes()?.ToList();
            Type type = types?.Find(a => targetType.IsAssignableFrom(a));

            if (type == null)
                return null;

            return argsForInstance == null
                ? Activator.CreateInstance(type)
                : Activator.CreateInstance(type, argsForInstance);
        }
    }
}