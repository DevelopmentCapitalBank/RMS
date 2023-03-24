﻿using RMS.DATA;
using RMS.DATA.Entities;

namespace DebugConsole
{
    public class Program 
    {
        public static void Main(string[] args)
        {
            TestDb();
            Console.WriteLine("End.");
        }

        public static void TestDb()
        {
            DbConfig dbConfig = new();
            dbConfig.Name = @"Data Source=H:\Perkin\md.db;";

            DbContext context = new(dbConfig);
            try
            {
                context.Setup();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

