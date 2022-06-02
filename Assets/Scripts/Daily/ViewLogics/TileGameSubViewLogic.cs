using FairyGUI;
using Daily;
using System.Collections.Generic;
using UnityEngine;

public class TileGameSubViewLogic
{
    enum Value { Empty, Two, Four, Eight }
    UI_Page view;
    DailySystemLogic systemLogic;
    UI_TileGame tileGame;
    List<UI_SingleTile> tileList;
    List<List<Vector2>> coordinateList;
    List<List<bool>> occupiedList;

    public TileGameSubViewLogic(UI_Page view, DailySystemLogic systemLogic)
    {
        this.view = view;
        this.systemLogic = systemLogic;
    }

    public void SetupTiles()
    {
        InitializeCoordinateList();
        InitializeOccupiedList();

        if (view.GetChild("TileGame") == null) return;
        tileGame = view.GetChild("TileGame") as UI_TileGame;

        tileList = new List<UI_SingleTile>();
        foreach (var child in tileGame._children)
        {
            if (child.name.Length < 4) continue;
            if (child.name.Substring(0, 4).Equals("Tile"))
            {
                var tile = child as UI_SingleTile;
                tile.active = false;

                tileList.Add(tile);
            }
        }

        tileGame.onKeyDown.Add(GameOnKeyDown);

        SpawnTile();
    }

    public void GameOnKeyDown(EventContext context)
    {
        // TODO 
        if (context.inputEvent.keyCode == KeyCode.UpArrow)
        {
            Debug.Log("Up Pressed");
            SpawnTile();
        }
        else if (context.inputEvent.keyCode == KeyCode.DownArrow)
        {
            Debug.Log("Down Pressed");
            SpawnTile();
        }
        else if (context.inputEvent.keyCode == KeyCode.LeftArrow)
        {
            Debug.Log("Left Pressed");
            SpawnTile();
        }
        else if (context.inputEvent.keyCode == KeyCode.RightArrow)
        {
            Debug.Log("Right Pressed");
            SpawnTile();
        }
    }

    public bool CheckGameEnd()
    {
        foreach (var row in occupiedList)
        {
            foreach (var element in row)
            {
                if (element == false)
                {

                    return false;
                }
            }
        }

        // TODO : put occupiedList in DailyData, change to actual number values --> scalability, calculate score
        // TODO : put Check Game End in DailyData, then publish GameEnd message in the SystemLogic
        return true;
    }

    public void SpawnTile()
    {
        if (CheckGameEnd()) return;

        int row = Random.Range(0, 4);
        int col = Random.Range(0, 4);

        while (occupiedList[row][col] == true)
        {
            row = Random.Range(0, 4);
            col = Random.Range(0, 4);
        }

        occupiedList[row][col] = true;
        SetTile(new Vector2(row, col));
    }

    public void SetTile(Vector2 position)
    {
        foreach (var tile in tileList)
        {
            if (!tile.active)
            {
                tile.active = true;
                tile.position = ConvertCoordinates(position);
                tile.m_Value.selectedIndex = (int)Value.Two;
                break;
            }
        }
    }

    public void MoveTileTo(UI_SingleTile tile, Vector2 destination)
    {
        // TODO
    }

    public void UpdateOccupiedList()
    {
        // TODO
    }

    public void OnGameEnd(TileGameEnd obj)
    {
        // TODO : Go to finish page
    }

    private Vector2 ConvertCoordinates(Vector2 position)
    {
        return coordinateList[(int)position.x][(int)position.y];
    }

    private void InitializeCoordinateList()
    {
        coordinateList = new List<List<Vector2>>();

        var row1 = new List<Vector2>();
        row1.Add(new Vector2(75, 660));
        row1.Add(new Vector2(230, 660));
        row1.Add(new Vector2(385, 660));
        row1.Add(new Vector2(540, 660));
        coordinateList.Add(row1);

        var row2 = new List<Vector2>();
        row2.Add(new Vector2(75, 815));
        row2.Add(new Vector2(230, 815));
        row2.Add(new Vector2(385, 815));
        row2.Add(new Vector2(540, 815));
        coordinateList.Add(row2);

        var row3 = new List<Vector2>();
        row3.Add(new Vector2(75, 970));
        row3.Add(new Vector2(230, 970));
        row3.Add(new Vector2(385, 970));
        row3.Add(new Vector2(540, 970));
        coordinateList.Add(row3);

        var row4 = new List<Vector2>();
        row4.Add(new Vector2(75, 1125));
        row4.Add(new Vector2(230, 1125));
        row4.Add(new Vector2(385, 1125));
        row4.Add(new Vector2(540, 1125));
        coordinateList.Add(row4);
    }

    private void InitializeOccupiedList()
    {
        occupiedList = new List<List<bool>>();
        for (int i = 0; i < 4; i++)
        {
            var row1 = new List<bool>();
            for (int j = 0; j < 4; j++)
                row1.Add(false);
            occupiedList.Add(row1);
        }
    }

}