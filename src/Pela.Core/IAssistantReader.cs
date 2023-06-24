namespace Pela.Core
{
    public interface IAssistantReader
    {
        IAsyncEnumerable<Assistant> ReadAsync(string uri);
    }
}
