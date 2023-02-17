using RMS.DATA;
using RMS.DATA.Entities;

namespace DebugConsole
{
    public class Program 
    {
        public static void Main(string[] args)
        {
            DbConfig dbConfig = new DbConfig();
            dbConfig.Name = @"Data Source=H:\Perkin\md.db;";

            DbContext context = new DbContext(dbConfig);
            context.Setup();
            context.Groups.CreateAsync(new Group { Name = "ПУСТО", Comment = "Пустая группа, по умолчанию" });

            Console.WriteLine("Hello, World!");
        }
    }
}

