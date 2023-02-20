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

                Group newg = await context.Groups.CreateAsync(new Group { Name = "ПУСТО", Comment = "Пустая группа, по умолчанию" });

                Group g = await context.Groups.ReadByIdAsync(1);
                Console.WriteLine($"Name - {g.Name}, id - {g.GroupId}, comment - {g.Comment}");

                g.Name = "не пусто";
                g.Comment = "заполнил";

                //await context.Groups.UpdateAsync(g);

                List<Group> groups = (List<Group>)await context.Groups.ReadAllAsync().ConfigureAwait(false);
                foreach(var gr in groups)
                {
                    Console.WriteLine($"Name - {gr.Name}, id - {gr.GroupId}, comment - {gr.Comment}");
                }

                //await context.Groups.DeleteAsync(g);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

