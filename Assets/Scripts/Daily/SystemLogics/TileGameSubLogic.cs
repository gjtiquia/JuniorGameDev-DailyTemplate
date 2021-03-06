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

    public DailyTileGameData.TileBoard GetTileBoard()
    {
        return userData.tileGameData.board;
    }

    public bool CheckGameEnd()
    {
        if (userData.tileGameData.board.CheckGameEnd() == true)
        {
            messageHub.Publish<TileGameEnd>(null);
            return true;
        }
        return false;
    }
}
