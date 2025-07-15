using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.System
{
    internal class JsonLoader
    {
        public static string LoadJson(string path)
        {
            if (!File.Exists(path))
            {
                return "";

            }
            string json = File.ReadAllText(path);
            return json;
        }
    }
}
