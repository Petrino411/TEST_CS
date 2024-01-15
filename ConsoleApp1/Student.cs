using System.Drawing;
using System.Globalization;

namespace ConsoleApp1;

public class Student : IReader<Student>
{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public int EnterYear { get; set; }
    public DateTime LastExam { get; set; }


    private Student(string[] splittedLine)
    {
        try
        {
            this.Id = int.Parse(splittedLine[0]);
            this.Firstname = splittedLine[1];
            this.Lastname = splittedLine[2];
            this.EnterYear = int.Parse(splittedLine[3]);
            this.LastExam = DateTime.Parse(splittedLine[4], CultureInfo.InvariantCulture);
        }
        catch (Exception e)
        {
            throw new ArgumentException($"Неправильная входная строка: {e}");
        }
    }

    public override string ToString()
    {
        return $"{this.Id}, {this.Firstname}, {this.Lastname}, {this.EnterYear}, {this.LastExam.Date.ToShortDateString()}";
    }

    public static async IAsyncEnumerable<Student> Reader(string resourcesPath)
    {
        var file = new FileInfo(resourcesPath + @"\in.txt");
        var text = File.ReadAllLinesAsync(file.FullName);

        foreach (var line in await text)
        {
            var lineSplit = line.Split();
            yield return new Student(lineSplit);
        }
    }
}