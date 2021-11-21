using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace EjercicioOpenTK.common
{
    class TextFile
    {

        public static string getDirectoryName()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            path = path.Remove(0, 6);
            path = path + "..\\..\\..\\..\\files";
            return path;
        }

        public static void saveFileText(string text, string fileName)
        {

            string path = getDirectoryName();
            path = path + "\\" + fileName + ".txt";

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(text);
                }
            }
        }

        public static string getFileText(string fileName)
        {
            string path = getDirectoryName();
            path = path + "\\" + fileName + ".txt";

            string t = "";
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    t += s;
                }
            }
            return t;
        }

    }
}