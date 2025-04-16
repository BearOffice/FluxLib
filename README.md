# FluxLib
A library to support flux design.

## Flux design
```plaintext
+---------+        +-------------+        +--------+        +--------+
| Action  | -----> | Dispatcher  | -----> | Store  | -----> |  View  |
+---------+        +-------------+        +--------+        +--------+
     ^                                                       |
     |-------------------------------------------------------+
                   (User interaction triggers new Action)
```
1. User interacts with the View (e.g., clicking a button).
2. This triggers an Action.
3. The Dispatcher receives the action and sends it to the appropriate Store.
4. The Store updates its internal state based on the action.
5. The Store emits a change event.
6. The View listens for these changes and re-renders accordingly.
7. The cycle repeats as the user continues to interact.

## Explanation of this library
This library provides the following components: `DispatchCenter`, `IStore`, `ActionArgs`, and `DataArgs`.  

- `ActionArgs` is a class used to define the action type and the data required for that action. It facilitates communication in the flow: `View -> Dispatcher -> Store`.  
- `DataArgs` is a class used to define the data type and the data being passed. It facilitates communication in the flow: `Store -> View`.


1. The `DispatchCenter` class receives `ActionArgs` from the *View* and broadcasts them to all `IStore` instances that are listening to it.

### Example: Listening for an action in a *Store*
```csharp
// SimpleAction is an enum that defines various actions associated with `SimpleView`
_dispatchCenter.AddListener(typeof(SimpleAction), ActionReceived);  

private void ActionReceived(object? sender, ActionArgs e) { 
    ...
}
```

2. The *Store* processes the received action and emits a change event to all *Views* that are subscribed to it.

### Example: Emiting a change in a *Store*
```csharp
var data = new DataArgs();
data.AddData(SimpleTag.Text, str);

// Push the processed result to the subscribers
Changed?.Invoke(this, data);  // `Changed` is the event defined in `IStore`
```

### Example: Listening for changes in a *View*
```csharp
public SimpleView(DispatchCenter dispatchCenter, SimpleStore store)
{
    _dispatcherCenter = dispatchCenter;

    _store = store;
    _store.Changed += Store_Changed;  // Subscribe to store change events
}
```

3. The *View* sends actions to the `DispatchCenter`.

### Example: Sending an action to `DispatchCenter`
```csharp
var action = new ActionArgs(SimpleAction.Repeat);
action.AddData(SimpleTag.Text, input);

// Dispatch the action to all subscribers
_dispatcherCenter.DispatchEvent(action);
```
