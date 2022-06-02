using FairyGUI;
using Daily;
using System;

public class TicTacToeSubViewLogic
{

    UI_Page view;
    DailySystemLogic systemLogic;
    UI_TicTacToe ticTacToe;

    private enum CurrentPlayer { Circle, Cross };
    private CurrentPlayer currentPlayer;

    public TicTacToeSubViewLogic(UI_Page view, DailySystemLogic systemLogic)
    {
        this.view = view;
        this.systemLogic = systemLogic;
    }

    public void SetupGame()
    {
        SetupTiles();
        currentPlayer = CurrentPlayer.Circle;
    }

    public void SetupTiles()
    {
        int i = 1;

        if (view.GetChild("TicTacToe") == null) return;

        ticTacToe = view.GetChild("TicTacToe") as UI_TicTacToe;

        while (ticTacToe.GetType().GetField($"m_Tile_{i}") != null)
        {
            var tile = ticTacToe.GetType().GetField($"m_Tile_{i}").GetValue(ticTacToe) as GButton;
            tile.onClick.Add(TileOnClick);

            i++;
        }
    }

    public void TileOnClick(EventContext context)
    {
        var tile = context.sender as UI_Tile;
        tile.touchable = false;

        if (currentPlayer == CurrentPlayer.Circle)
        {
            tile.m_state.selectedIndex = 1;
            currentPlayer = CurrentPlayer.Cross;
        }
        else
        {
            tile.m_state.selectedIndex = 2;
            currentPlayer = CurrentPlayer.Circle;
        }

        int row = GetTileRow(tile);
        int col = GetTileColumn(tile);
        int element = currentPlayer == CurrentPlayer.Circle ? 0 : 1;
        systemLogic.SetTicTacToeTile(row, col, element);
    }

    public void OnGameOver(TicTacToeGameOver obj)
    {
        var transition = view.GetTransition("MiniGameCorrect");
        if (transition != null)
        {
            transition.Play(() =>
            {
                view.m_Page.selectedIndex = 4; // go to ending page (Page 4))
            }
            );
        }
        else
        {
            view.m_Page.selectedIndex = 4; // go to ending page (Page 4)
        }
    }

    private int GetTileRow(UI_Tile tile)
    {
        int num = (Int32.Parse(tile.name[tile.name.Length - 1].ToString()) - 1) / 3;
        return num;
    }
    private int GetTileColumn(UI_Tile tile)
    {
        int num = (Int32.Parse(tile.name[tile.name.Length - 1].ToString()) - 1) % 3;
        return num;
    }
}