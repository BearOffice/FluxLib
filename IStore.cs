namespace FluxLib;

public interface IStore : IDisposable
{
    public event EventHandler<DataArgs>? Changed;
    public DataArgs GetData();
}
