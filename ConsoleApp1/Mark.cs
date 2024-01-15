namespace ConsoleApp1;

public class Mark : IReader<Mark>
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public Discipline DisciplineId { get; set; }
    public int DisciplineMark { get; set; }

    private Mark(string[] splittedLine)
    {
        try
        {
            this.Id = int.Parse(splittedLine[0]);
            this.StudentId = int.Parse(splittedLine[1]);
            this.DisciplineId = Enum.GetValues<Discipline>().ElementAt(int.Parse(splittedLine[2]));
            this.DisciplineMark = int.Parse(splittedLine[3]);
        }
        catch (Exception e)
        {
            throw new ArgumentException($"Неправильная входная строка: {e}");
        }
    }

    public static async IAsyncEnumerable<Mark> Reader(string resourcesPath)
    {
        var file = new FileInfo(resourcesPath + @"\marks.txt");
        var text = File.ReadAllLinesAsync(file.FullName);

        foreach (var line in await text)
        {
            var lineSplit = line.Split();
            yield return new Mark(lineSplit);
        }
        
    }
}