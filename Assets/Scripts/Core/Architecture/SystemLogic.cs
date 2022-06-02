using Easy.MessageHub;

public abstract class SystemLogic : ISystemLogic
{
    protected IMessageHub messageHub;

    public SystemLogic(IMessageHub hub)
    {
        messageHub = hub;
    }
}