using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

using System.IO;

namespace NewBridge.Zoro.FileSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = ConfigurationManager.AppSettings["path"];
            try
            {
                List<string> results = new List<string>();
                SearchFile(path,results);
                if (results.Count > 0)
                {
                    using (StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["result"]))
                    {
                        foreach(string file in results)
                        sw.WriteLine(file);
                    }
                    Console.WriteLine("完毕，共处理文件{0}个",results.Count);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static void SearchFile(string path,List<string> results)
        {
            var dicts = Directory.EnumerateDirectories(path);
            foreach (var dict in dicts)
            {
                SearchFile(dict,results);
            }
            var files = Directory.EnumerateFiles(path);

                foreach (var file in files)
                {
                    Console.WriteLine(file);
                    results.Add(file);
                }
        }
    }
}
