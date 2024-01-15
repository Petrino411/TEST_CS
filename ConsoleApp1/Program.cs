namespace ConsoleApp1;

class Program
{
    private static IList<Student> Students = new List<Student>();
    private static IList<Mark> StMarks = new List<Mark>();

    private static string resourcesPath = AppDomain.CurrentDomain.BaseDirectory.Contains(@"\bin\") ? 
        AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf(@"\bin\")) +
        @"\Resources" : AppDomain.CurrentDomain.BaseDirectory;

    static async Task Main(string[] args)
    {
        var tasks = new List<Task>();

        tasks.Add(Task.Run(()=> Awaiter(Student.Reader(resourcesPath), Students)));
        tasks.Add(Task.Run(()=> Awaiter(Mark.Reader(resourcesPath), StMarks)));

        await Task.WhenAll(tasks);
        foreach (var line in Students.Where(x => StMarks.Where(y => y.DisciplineMark >= 3).Any(y => y.StudentId == x.Id)))
        {
            Console.WriteLine(line);
        }
        
    }
    
    private static async Task Awaiter<T>(IAsyncEnumerable<T> lst, IList<T> targetList) where T : IReader<T>
    {
        try
        {
            targetList ??= new List<T>();
            await foreach (var row in lst)
            {
                targetList.Add(row);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}