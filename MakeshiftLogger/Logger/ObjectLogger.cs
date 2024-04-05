namespace DCT.TraineeTasks.MakeshiftLogger.Logger;

public class ObjectLogger(string path) : IDisposable, IAsyncDisposable
{
    private FileStream LogStream { get; } = new(path, FileMode.Append);

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

    protected virtual void Dispose(bool disposing)
    {
        if (disposing) LogStream.Dispose();
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        await LogStream.DisposeAsync();
    }
}