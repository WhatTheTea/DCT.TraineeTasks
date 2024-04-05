namespace DCT.TraineeTasks.MakeshiftLogger.Logger;

public sealed class ObjectLogger : IDisposable, IAsyncDisposable
{
    public ObjectLogger(string path)
    {
        LogStream = new StreamWriter(path);
        Console.WriteLine("Object logger is instantiated!");
    }

    private StreamWriter LogStream { get; }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore();
        GC.SuppressFinalize(this);
        Console.WriteLine("ObjectLogger is disposed asynchronously!");
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
        Console.WriteLine("ObjectLogger is disposed!");
    }

    ~ObjectLogger()
    {
        Dispose(false);
    }

    public async Task LogInfoAsync<T>(T obj)
    {
        await LogStream.WriteLineAsync($"{DateTime.Now} | INFO | {obj?.ToString()} ");
    }

    public void LogInfo<T>(T obj)
    {
        LogInfoAsync(obj)
            .GetAwaiter().GetResult();
    }

    private void Dispose(bool disposing)
    {
        if (disposing) LogStream.Dispose();
    }

    private async ValueTask DisposeAsyncCore()
    {
        await LogStream.DisposeAsync();
    }
}