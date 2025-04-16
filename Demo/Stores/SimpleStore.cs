using Demo.Actions;
using Demo.DataTags;

namespace Demo.Stores;

public class SimpleStore : IStore
{
    public event EventHandler<DataArgs>? Changed;
    private readonly DispatchCenter _dispatchCenter;

    public SimpleStore(DispatchCenter dispatchCenter)
    {
        _dispatchCenter = dispatchCenter;

        // Listen the specified action: SimpleAction
        _dispatchCenter.AddListener(typeof(SimpleAction), ActionReceived);
    }

    private void ActionReceived(object? sender, ActionArgs e)
    {
        if (e.Type is SimpleAction.Repeat)
        {
            var str = (string)e.GetData(SimpleTag.Text)!;
            var data = new DataArgs();
            data.AddData(SimpleTag.Text, str + str);  // Process the data received from action

            // Push the processed result to the subscribers
            Changed?.Invoke(this, data);
        }
    }

    public void Dispose()
    {
        _dispatchCenter.RemoveListener(ActionReceived);

        Changed.UnsubscribeAll();
        Changed = null;

        GC.SuppressFinalize(this);
    }

    public DataArgs GetData()
    {
        var data = new DataArgs();
        data.AddData(SimpleTag.InitMessage, "Hello, the time is: " + DateTime.Now.ToString());

        return data;
    }
}
