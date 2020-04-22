using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Infrastructure.Enums;

namespace Infrastructure.FileHelpers
{
    public static class ReadWriteFileHelper
    {
        public static void WriteLogToFile(string fileName, Exception ex)
        {
            try
            {
                var file = string.Concat(fileName, $" {DateTime.Now:MM-dd-yyyy hh-mm-ss}", ".txt");
                string path = CreateDictionary(DictionaryEnum.Log, file);
                if (!File.Exists(path))
                {
                    using (var sw = File.CreateText(path))
                    {
                        sw.WriteLine(ex.GetType().FullName);
                        sw.WriteLine("Message : " + ex.Message);
                        sw.WriteLine("StackTrace : " + ex.StackTrace);
                    }
                }
                else
                {
                    using (var sw = File.AppendText(path))
                    {
                        sw.WriteLine("Message : " + ex.Message);
                        sw.WriteLine("StackTrace : " + ex.StackTrace);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void WriteLogToFile(DictionaryEnum folder, string fileName, string message)
        {
            try
            {
                string path = CreateDictionary(folder, fileName);
                if (!File.Exists(path))
                {
                    using (var sw = File.CreateText(path))
                    {
                        sw.WriteLineAsync(message);
                    }
                }
                else
                {
                    using (var sw = File.AppendText(path))
                    {
                        sw.WriteLineAsync(message);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static string CreateDictionary(DictionaryEnum folder, string fileName)
        {
            string path = Path.GetPathRoot(Environment.SystemDirectory);
       //     var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = string.Concat(path, folder);
            if (folder == DictionaryEnum.Log)
                path = string.Concat(path, "\\", DateTime.Now.ToString("MM-dd-yyyy"));
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = string.Concat(path, "\\", fileName);
            return path;
        }
    }
}