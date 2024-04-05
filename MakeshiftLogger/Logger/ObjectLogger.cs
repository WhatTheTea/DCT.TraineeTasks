namespace DCT.TraineeTasks.MakeshiftLogger.Logger;

public class ObjectLogger(string path) : IDisposable, IAsyncDisposable
{
    private StreamWriter LogStream { get; } = new(path);

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore();
        GC.SuppressFinalize(this);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
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

    protected virtual void Dispose(bool disposing)
    {
        if (disposing) LogStream.Dispose();
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        await LogStream.DisposeAsync();
    }
}