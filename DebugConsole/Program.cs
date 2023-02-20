using RMS.DATA;
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

        public static async void TestDb()
        {
            DbConfig dbConfig = new();
            dbConfig.Name = @"Data Source=H:\Perkin\md.db;";

            DbContext context = new(dbConfig);
            try
            {
                await context.Setup();

                DateOp newDate = new();
                newDate.DateOperation = DateTime.Now;

                DateOp d = await context.DateOps.CreateAsync(newDate);
                Console.WriteLine($"Date id = {d.DateId}, date - {d.DateOperation}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

