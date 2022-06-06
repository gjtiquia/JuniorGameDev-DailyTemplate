using FairyGUI;
using Daily;
using System.Collections.Generic;
using UnityEngine;

public class TileGameSubViewLogic
{
    enum Value { Empty, Num2, Num4, Num8, Num16, Num32, Num64, Num128, Num256 }
    enum Direction { Up, Down, Left, Right }
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
                tile._active = false;

                tileList.Add(tile);
            }
        }

        tileGame.onKeyDown.Add(GameOnKeyDown);

        SpawnTile();
        // Test();
    }

    public void Test()
    {
        // SetTile(new Vector2(0, 0), 2);
        // SetTile(new Vector2(0, 1), 8);
        // SetTile(new Vector2(0, 2), 2);
        // SetTile(new Vector2(0, 3), 2);

        // SetTile(new Vector2(1, 0), 2);
        // SetTile(new Vector2(1, 1), 2);
        // SetTile(new Vector2(1, 2), 2);
        // SetTile(new Vector2(1, 3), 2);

        // SetTile(new Vector2(2, 0), 2);
        // SetTile(new Vector2(2, 1), 4);
        // SetTile(new Vector2(2, 2), 2);
        // SetTile(new Vector2(2, 3), 2);

        // SetTile(new Vector2(3, 0), 2);
        // SetTile(new Vector2(3, 1), 2);
        // SetTile(new Vector2(3, 2), 2);
        // SetTile(new Vector2(3, 3), 2);

        Debug.Log(tileBoard);
    }

    public void GameOnKeyDown(EventContext context)
    {
        // TODO 
        if (context.inputEvent.keyCode == KeyCode.UpArrow)
        {
            Debug.Log("Up Pressed");
        }
        else if (context.inputEvent.keyCode == KeyCode.DownArrow)
        {
            Debug.Log("Down Pressed");
        }
        else if (context.inputEvent.keyCode == KeyCode.LeftArrow)
        {
            Debug.Log("Left Pressed");
            MoveLeft();
            Debug.Log(tileBoard);
        }
        else if (context.inputEvent.keyCode == KeyCode.RightArrow)
        {
            Debug.Log("Right Pressed");
            MoveRight();
            Debug.Log(tileBoard);
        }
    }

    public bool CheckGameEnd()
    {
        return systemLogic.TileGameCheckGameEnd();
    }

    public void SpawnTile()
    {
        CheckGameEnd();

        Vector2 position = tileBoard.SpawnTile();

        SetTile(position, 2);
    }

    public void SetTile(Vector2 position, int number)
    {
        tileBoard.SetNumber(position, number);

        foreach (var tile in tileList)
        {
            if (!tile._active)
            {
                tile._active = true;
                tile._number = number;
                tile._boardPosition = position;
                tile._newlyFormed = false;

                tile.position = ConvertCoordinates(position);

                // TODO : is there a better solution?
                tile.TweenMove(ConvertCoordinates(position), 0.1f);

                tile.m_Value.selectedIndex = (int)Mathf.Log(number, 2);

                var transition = tile.GetTransition("Show");
                transition.Play();



                return;
            }
        }
    }

    public void DestroyTile(UI_SingleTile tile)
    {
        tile.TweenMove(tile.position, 0.1f);
        tile._active = false;
        tile._number = 0;
        tile._boardPosition = new Vector2(-1, -1);
        tile.m_Value.selectedIndex = (int)Value.Empty;
    }

    public void IncreaseTileNumber(UI_SingleTile tile)
    {
        tile._number *= 2;
        tile._newlyFormed = true;
        var index = tile.m_Value.selectedIndex;
        tile.m_Value.selectedIndex = index + 1;

        var transition = tile.GetTransition("Increase");
        transition.Play();
    }

    public void MoveTileTo(UI_SingleTile tile, Vector2 destination)
    {
        Vector2 coordinate = ConvertCoordinates(destination);
        tile.TweenMove(coordinate, tweenMoveDuration);
        tile._boardPosition = destination;
    }

    public void MoveRight()
    {
        bool moved = false;

        for (int col = 2; col >= 0; col--)
        {
            for (int row = 0; row < 4; row++)
            {
                Vector2 position = new Vector2(row, col);
                if (!tileBoard.IsOccupied(position)) continue;
                UI_SingleTile tile = GetTile(position);

                if (tileBoard.HaveSameNumberOnRight(position, tile._number))
                {
                    Vector2 newPosition = tileBoard.GetSameNumberPositionOnRight(position, tile._number);
                    UI_SingleTile targetTile = GetTile(newPosition);

                    // Check if target tile was newly formed
                    if (targetTile.IsNewlyFormed())
                    {
                        moved = MoveToUnoccupiedSpaceOnRight(tile, position, moved);
                    }
                    else
                    {
                        tileBoard.SetNumber(position, 0);

                        MoveTileTo(tile, newPosition);
                        DestroyTile(targetTile);
                        IncreaseTileNumber(tile);

                        tileBoard.SetNumber(newPosition, tile._number);

                        moved = true;
                    }
                }

                else
                {
                    moved = MoveToUnoccupiedSpaceOnRight(tile, position, moved);
                }
            }
        }

        if (moved)
        {
            ResetNewlyFormed();
            SpawnTile();
        }
        else
        {
            CheckGameEnd();
        }
    }

    public bool MoveToUnoccupiedSpaceOnRight(UI_SingleTile tile, Vector2 position, bool moved)
    {
        if (tileBoard.HaveUnoccupiedSpaceOnRight(position))
        {
            Vector2 newPosition = tileBoard.GetUnoccupiedPositionOnRight(position);

            tileBoard.SetNumber(position, 0);
            tileBoard.SetNumber(newPosition, tile._number);

            MoveTileTo(tile, newPosition);

            return true;
        }

        return moved;
    }

    private bool MoveToUnoccupiedSpace(Direction direction, UI_SingleTile tile, Vector2 position, bool moved)
    {
        // TODO
        bool haveUnoccupiedSpace = false;
        if (direction == Direction.Right) haveUnoccupiedSpace = tileBoard.HaveUnoccupiedSpaceOnRight(position);

        if (tileBoard.HaveUnoccupiedSpaceOnRight(position))
        {
            Vector2 newPosition = tileBoard.GetUnoccupiedPositionOnRight(position);

            tileBoard.SetNumber(position, 0);
            tileBoard.SetNumber(newPosition, tile._number);

            MoveTileTo(tile, newPosition);

            return true;
        }

        return moved;
    }

    public void MoveLeft()
    {
        bool moved = false;

        for (int col = 1; col < 4; col++)
        {
            for (int row = 0; row < 4; row++)
            {
                Vector2 position = new Vector2(row, col);
                if (!tileBoard.IsOccupied(position)) continue;
                UI_SingleTile tile = GetTile(position);

                if (tileBoard.HaveSameNumberOnLeft(position, tile._number))
                {
                    Vector2 newPosition = tileBoard.GetSameNumberPositionOnLeft(position, tile._number);
                    UI_SingleTile targetTile = GetTile(newPosition);

                    // Check if target tile was newly formed
                    if (targetTile.IsNewlyFormed())
                    {
                        moved = MoveToUnoccupiedSpaceOnLeft(tile, position, moved);
                    }
                    else
                    {
                        tileBoard.SetNumber(position, 0);

                        MoveTileTo(tile, newPosition);
                        DestroyTile(targetTile);
                        IncreaseTileNumber(tile);

                        tileBoard.SetNumber(newPosition, tile._number);

                        moved = true;
                    }
                }

                else
                {
                    moved = MoveToUnoccupiedSpaceOnLeft(tile, position, moved);
                }
            }
        }

        if (moved)
        {
            ResetNewlyFormed();
            SpawnTile();
        }
        else
        {
            CheckGameEnd();
        }
    }

    public bool MoveToUnoccupiedSpaceOnLeft(UI_SingleTile tile, Vector2 position, bool moved)
    {
        if (tileBoard.HaveUnoccupiedSpaceOnLeft(position))
        {
            Vector2 newPosition = tileBoard.GetUnoccupiedPositionOnLeft(position);

            tileBoard.SetNumber(position, 0);
            tileBoard.SetNumber(newPosition, tile._number);

            MoveTileTo(tile, newPosition);

            return true;
        }

        return moved;
    }

    public void ResetNewlyFormed()
    {
        foreach (var tile in tileList)
        {
            if (!tile._active) continue;
            if (tile.IsNewlyFormed())
            {
                tile._newlyFormed = false;
            }
        }
    }

    public UI_SingleTile GetTile(Vector2 position)
    {
        foreach (var tile in tileList)
        {
            if (!tile._active) continue;
            if (tile._boardPosition == position)
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