using FairyGUI;
using Daily;
using System;

public class TileGameSubViewLogic
{

    UI_Page view;
    DailySystemLogic systemLogic;
    UI_TileGame tileGame;

    public TileGameSubViewLogic(UI_Page view, DailySystemLogic systemLogic)
    {
        this.view = view;
        this.systemLogic = systemLogic;
    }

    public void SetupTiles()
    {
        // TODO : Random generate one tile
    }

    public void OnGameEnd(TileGameEnd obj)
    {
        // TODO : Go to finish page
    }
}