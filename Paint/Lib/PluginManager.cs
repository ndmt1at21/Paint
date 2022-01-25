using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PluginContract;
using System.Windows;
using System.Windows.Media;

namespace Paint.Lib
{
    public class PluginManager
    {
        private Dictionary<string, INode> prototype { get; set; } =
            new Dictionary<string, INode>();

        public PluginManager(string path)
        {
            prototype = new Dictionary<string, INode>();
            Load(path);
        }

        private void Load(string path)
        {
            FileInfo[] files = Utils
                .Dll
                .GetDllFilesFromFolder(path)
                .Where(file => file.Directory.Name != "ref").ToArray();

            foreach (FileInfo file in files)
            {
                INode plugin = (INode)Utils.Dll.CreateInstanceFromDllFile(file.FullName, typeof(INode));

                if (plugin == null)
                    continue;

                if (!prototype.ContainsKey(plugin.ID))
                    prototype.Add(plugin.Name, plugin);
            }
        }

        public IShapeNode CreateShape(string id)
        {
            if (prototype[id] is IShapeNode)
            {
                return (IShapeNode)prototype[id];
            }

            return null;
        }


        public string[] GetPluginIDs()
        {
            return prototype.Keys.ToArray();
        }
    }
}