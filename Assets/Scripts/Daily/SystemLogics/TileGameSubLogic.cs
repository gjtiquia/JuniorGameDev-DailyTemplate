using Easy.MessageHub;

public class TileGameSubLogic
{
    DailyData userData;
    IMessageHub messageHub;

    public TileGameSubLogic(DailyData userData, IMessageHub hub)
    {
        this.userData = userData;
        this.messageHub = hub;
    }

}
