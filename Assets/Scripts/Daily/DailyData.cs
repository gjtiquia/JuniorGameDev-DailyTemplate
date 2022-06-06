using System;
using System.Collections.Generic;
using UnityEngine;

public class DailyData
{
    public DailyDragAndDropData dragAndDropData = new DailyDragAndDropData();
    public DailyDragAndDropInOrderData dragAndDropInOrderData = new DailyDragAndDropInOrderData();
    public DailyDrawLineData drawLineData = new DailyDrawLineData();
    public DailySpotDifferenceData spotDifferenceData = new DailySpotDifferenceData();
    public DailyFlipCardData flipCardData = new DailyFlipCardData();
    public DailyDragDropMatchData dragDropMatchData = new DailyDragDropMatchData();
    public DailyTreasureHuntData treasureHuntData = new DailyTreasureHuntData();
    public DailyTicTacToeData ticTacToeData = new DailyTicTacToeData();
    public DailyTileGameData tileGameData = new DailyTileGameData();
}

public class DailyDragAndDropData
{
    public int correctCount = 0;
    public int allCorrectCount = 0;

    public void Reset()
    {
        correctCount = 0;
        allCorrectCount = 0;
    }
}

public class DailyDragAndDropInOrderData
{
    public int correctCount = 0;
    public int allCorrectCount = 0;
}

public class DailyDrawLineData
{
    public int correctCount = 0;

    public int allCorrectCount = 12;
}

public class DailySpotDifferenceData
{
    public int correctCount = 0;
    public int allCorrectCount = 0;
}

public class DailyFlipCardData
{
    public int correctCount = 0;
    public int allCorrectCount = 0;
}

public class DailyDragDropMatchData
{
    public int correctCount = 0;
    public int allCorrectCount = 0;
}

public class DailyTreasureHuntData
{
    public int correctCount = 0;
    public int allCorrectCount = 0;
}

public class DailyTicTacToeData
{
    public Board board;

    public DailyTicTacToeData()
    {
        board = new Board();
    }

    public class Board
    {
        public List<List<int>> boardArray;

        public Board()
        {
            this.boardArray = new List<List<int>>();

            List<int> emptyRow = new List<int>();
            emptyRow.Add(-1);
            emptyRow.Add(-2);
            emptyRow.Add(-3);
            this.boardArray.Add(emptyRow);
            List<int> emptyRow2 = new List<int>();
            emptyRow2.Add(-4);
            emptyRow2.Add(-5);
            emptyRow2.Add(-6);
            this.boardArray.Add(emptyRow2);
            List<int> emptyRow3 = new List<int>();
            emptyRow3.Add(-7);
            emptyRow3.Add(-8);
            emptyRow3.Add(-9);
            this.boardArray.Add(emptyRow3);
        }

        public string SetTile(int row, int col, int element)
        {
            boardArray[row][col] = element;
            return "[" + row + "][" + col + "] = " + element + "\n" + this.ToString();
        }

        public bool CheckWin()
        {
            return (RowCrossed() || ColumnCrossed() || DiagonalCrossed());
        }

        private bool RowCrossed()
        {
            for (int i = 0; i < 3; i++)
            {
                if (boardArray[i][0] == boardArray[i][1] &&
                    boardArray[i][1] == boardArray[i][2] &&
                    boardArray[i][0] >= 0)
                    return true;
            }
            return false;
        }

        private bool ColumnCrossed()
        {
            for (int i = 0; i < 3; i++)
            {
                if (boardArray[0][i] == boardArray[1][i] &&
                    boardArray[1][i] == boardArray[2][i] &&
                    boardArray[0][i] >= 0)
                    return (true);
            }

            return false;
        }

        private bool DiagonalCrossed()
        {
            if (boardArray[0][0] == boardArray[1][1] &&
                boardArray[1][1] == boardArray[2][2] &&
                boardArray[0][0] >= 0)
                return (true);

            if (boardArray[0][2] == boardArray[1][1] &&
                boardArray[1][1] == boardArray[2][0] &&
                boardArray[0][2] >= 0)
                return (true);

            return false;
        }

        public override string ToString()
        {
            var output = "[";

            for (int i = 0; i < 3; i++)
            {
                var i0 = boardArray[i][0].ToString();
                var i1 = boardArray[i][1].ToString();
                var i2 = boardArray[i][2].ToString();
                output += "[" + i0 + ", " + i1 + ", " + i2 + "]";
                output += i < 2 ? "\n" : "";
            }

            output += "]";

            return output;
        }
    }
}

public class DailyTileGameData
{
    public TileBoard board;

    public DailyTileGameData()
    {
        board = new TileBoard();
    }

    public class TileBoard
    {
        List<List<int>> boardArray;

        public TileBoard()
        {
            boardArray = new List<List<int>>();
            for (int i = 0; i < 4; i++)
            {
                var row = new List<int>();
                for (int j = 0; j < 4; j++)
                {
                    row.Add(0);
                }
                boardArray.Add(row);
            }
        }

        public bool CheckGameEnd()
        {
            foreach (var row in boardArray)
            {
                foreach (var element in row)
                {
                    if (element == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool IsOccupied(Vector2 position)
        {
            int row = (int)position.x;
            int col = (int)position.y;
            if (boardArray[row][col] == 0) return false;
            return true;
        }

        public void SetTile(Vector2 position)
        {
            int row = (int)position.x;
            int col = (int)position.y;
            boardArray[row][col] = 2;
        }

        public Vector2 SpawnTile()
        {
            int row = UnityEngine.Random.Range(0, 4);
            int col = UnityEngine.Random.Range(0, 4);
            Vector2 position = new Vector2(row, col);

            while (IsOccupied(position))
            {
                row = UnityEngine.Random.Range(0, 4);
                col = UnityEngine.Random.Range(0, 4);
                position = new Vector2(row, col);
            }

            SetTile(position);

            return position;
        }
    }
}