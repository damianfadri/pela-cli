namespace Pela.Core
{
    public interface IAreaReader
    {
        IAsyncEnumerable<Area> ReadAsync(string uri);
    }
}
