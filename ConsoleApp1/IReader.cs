namespace ConsoleApp1;

public interface IReader<T>
{
    static abstract IAsyncEnumerable<T> Reader(string resourcesPath);
}