namespace DCT.TraineeTasks.MakeshiftLogger.Logger;

public sealed class ObjectLogger(string path) : IDisposable, IAsyncDisposable
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

    private void ReleaseUnmanagedResources()
    {
        // TODO release unmanaged resources here
    }

    ~ObjectLogger()
    {
        Dispose(false);
    }


    private void Dispose(bool disposing)
    {
        ReleaseUnmanagedResources();
        if (disposing) LogStream.Dispose();
    }

    private async ValueTask DisposeAsyncCore()
    {
        ReleaseUnmanagedResources();

        await LogStream.DisposeAsync();
    }
}