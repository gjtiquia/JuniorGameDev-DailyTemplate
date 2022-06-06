using FairyGUI;
using Daily;
using System.Collections.Generic;
using UnityEngine;

public class TileGameSubViewLogic
{
    enum Value { Empty, Num2, Num4, Num8, Num16, Num32, Num64, Num128, Num256 }
    UI_Page view;
    DailySystemLogic systemLogic;
    UI_TileGame tileGame;
    List<UI_SingleTile> tileList;
    List<List<Vector2>> coordinateList;
    DailyTileGameData.TileBoard tileBoard;
    float tweenMoveDuration = 0.1f;

    public TileGameSubViewLogic(UI_Page view, DailySystemLogic systemLogic)
    {
        this.view = view;
        this.systemLogic = systemLogic;
        this.tileBoard = systemLogic.GetTileBoard();
    }

    public void SetupTiles()
    {
        InitializeCoordinateList();

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
            MoveRight();
            SpawnTile();
        }
    }

    public bool CheckGameEnd()
    {
        return systemLogic.TileGameCheckGameEnd();
    }

    public void SpawnTile()
    {
        // TODO : Wait till move animations have finished
        // TODO : Spawn Animation

        Vector2 position = tileBoard.SpawnTile();

        foreach (var tile in tileList)
        {
            if (!tile.active)
            {
                tile.active = true;
                tile.number = 2;
                tile.position = ConvertCoordinates(position);
                tile.boardPosition = position;
                tile.m_Value.selectedIndex = (int)Value.Num2;
                break;
            }
        }

        CheckGameEnd();
    }

    public void DestroyTile(UI_SingleTile tile)
    {
        tile.active = false;
        tile.number = 0;
        tile.boardPosition = new Vector2(-1, -1);
        tile.m_Value.selectedIndex = (int)Value.Empty;
    }

    public void IncreaseTileNumber(UI_SingleTile tile)
    {
        tile.number *= 2;
        var index = tile.m_Value.selectedIndex;
        tile.m_Value.selectedIndex = index + 1;

        // TODO : Enlarge animation
    }

    public void MoveTileTo(UI_SingleTile tile, Vector2 destination)
    {
        Vector2 coordinate = ConvertCoordinates(destination);
        tile.TweenMove(coordinate, tweenMoveDuration);
        tile.boardPosition = destination;
    }

    public void MoveRight()
    {
        // Tiles on the rightmost column does not need to move
        // Start checking from the second rightmost column, leftwards
        for (int col = 2; col >= 0; col--)
        {
            for (int row = 0; row < 4; row++)
            {
                Vector2 position = new Vector2(row, col);
                if (!tileBoard.IsOccupied(position)) continue;
                UI_SingleTile tile = GetTile(position);

                // Check if have any same numbers on the right
                if (tileBoard.HaveSameNumberOnRight(position, tile.number))
                {
                    // Get position of the same number on the right
                    Vector2 newPosition = tileBoard.GetSameNumberPositionOnRight(position, tile.number);
                    UI_SingleTile overlappedTile = GetTile(newPosition);

                    // Move the tile to the right
                    MoveTileTo(tile, newPosition);

                    // Set overlapped tile active = false
                    DestroyTile(overlappedTile);

                    // Update Tile Number (maybe with enlarge animation)
                    IncreaseTileNumber(tile);


                    // Update tileBoard => position = 0, newPosition = updated number
                    tileBoard.SetNumber(position, 0);
                    tileBoard.SetNumber(newPosition, tile.number);
                }

                // Check if have any unoccupied space between
                else if (tileBoard.HaveUnoccupiedSpaceOnRight(position))
                {
                    // Get position of rightmost unoccupied space
                    Vector2 newPosition = tileBoard.GetUnoccupiedPositionOnRight(position);

                    // Move the tile to the right
                    MoveTileTo(tile, newPosition);

                    // Update tileBoard => position = 0, newPosition = tile.number
                    tileBoard.SetNumber(position, 0);
                    tileBoard.SetNumber(newPosition, tile.number);
                }

            }
        }
    }

    public UI_SingleTile GetTile(Vector2 position)
    {
        foreach (var tile in tileList)
        {
            if (!tile.active) continue;
            if (tile.boardPosition == position)
            {
                return tile;
            }
        }

        return null;
    }

    public void OnGameEnd(TileGameEnd obj)
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

}