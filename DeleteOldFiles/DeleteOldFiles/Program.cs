using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;


namespace DeleteOldFiles
{
    class Program
    {        
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine($"Starting Deleted Old Files in " + DateTime.Now);
                var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true);
                var config = builder.Build();

                var period = config.GetSection("config:period").Value;
                int periodConverted = Convert.ToInt32(period);
                periodConverted = periodConverted * -1;
                var folder = config.GetSection("config:folder").Value;
                Console.WriteLine($"period is: {period}");
                Console.WriteLine($"folder is: {folder}");

                string[] files = Directory.GetFiles(folder);

                foreach (string file in files)
                {
                    FileInfo fi = new FileInfo(file);
                    if (fi.LastWriteTime < DateTime.Now.AddDays(periodConverted))
                    {
                        Console.WriteLine($"File deleted: {fi.Name}");
                        fi.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error at try to deleted Old files: {ex.Message}");                
            }                        
        }
    }
}
