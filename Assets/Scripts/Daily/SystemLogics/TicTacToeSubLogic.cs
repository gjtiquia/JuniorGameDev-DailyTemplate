using Easy.MessageHub;

public class TicTacToeSubLogic
{
    DailyData userData;
    IMessageHub messageHub;

    public TicTacToeSubLogic(DailyData userData, IMessageHub hub)
    {
        this.userData = userData;
        this.messageHub = hub;
    }

    public void SetTile(int row, int col, int element) 
    {
        userData.ticTacToeData.board.SetTile(row, col, element);

        if (CheckWin())
        {
            messageHub.Publish<TicTacToeGameOver>(null);
        }
    }

    public bool CheckWin() 
    {
        return userData.ticTacToeData.board.CheckWin();
    }
}
