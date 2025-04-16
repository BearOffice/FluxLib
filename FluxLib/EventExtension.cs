namespace FluxLib;

public static class EventExtension
{
    public static void UnsubscribeAll(this EventHandler<DataArgs>? eventHandler)
    {
        if (eventHandler is null) return;

        foreach (var handler in eventHandler.GetInvocationList().Cast<EventHandler<DataArgs>>())
        {
            eventHandler -= handler;
        }
    }
}
