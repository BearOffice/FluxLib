using Demo.Actions;
using Demo.DataTags;
using Demo.Stores;

namespace Demo.Views;

public class SimpleView
{
    private readonly DispatchCenter _dispatcherCenter;
    private readonly SimpleStore _store;

    public SimpleView(DispatchCenter dispatchCenter, SimpleStore store)
    {
        _dispatcherCenter = dispatchCenter;
        _store = store;

        Store_Changed(null, _store.GetData());  // Initialize the view
        _store.Changed += Store_Changed;  // Listen the changes of store

        Loop();
    }

    private void Store_Changed(object? sender, DataArgs e)
    {
        if (e.TryGetData(SimpleTag.InitMessage, out var initMsg))
        {
            var item = (string)initMsg!;
            Console.WriteLine(item + "\n");  // Change the view
        }

        if (e.TryGetData(SimpleTag.Text, out var text))
        {
            var item = (string)text!;
            Console.WriteLine($"Repeated text is {item}\n");  // Change the view
        }
    }

    private void Loop()
    {
        while (true)
        {
            Console.Write("Input something: ");
            var input = Console.ReadLine()!;

            var action = new ActionArgs(SimpleAction.Repeat);
            action.AddData(SimpleTag.Text, input);

            // Send an action to the subscribers
            _dispatcherCenter.DispatchEvent(action);
        }
    }
}
