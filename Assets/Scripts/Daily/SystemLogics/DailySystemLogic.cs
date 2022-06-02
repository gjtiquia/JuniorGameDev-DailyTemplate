using Easy.MessageHub;
using System.Collections.Generic;

public class DailySystemLogic : SystemLogic
{
    DailyData userData = new DailyData();

    DragAndDropSubLogic dragAndDrop;
    RotationGameSubLogic rotationGame;
    SpotDifferenceSubLogic spotDifference;
    TicTacToeSubLogic ticTacToe;

    public DailySystemLogic(IMessageHub hub) : base(hub)
    {
        dragAndDrop = new DragAndDropSubLogic(userData, hub);
        rotationGame = new RotationGameSubLogic(userData, hub);
        spotDifference = new SpotDifferenceSubLogic(userData, hub);
        ticTacToe = new TicTacToeSubLogic(userData, hub);
    }

    public void SetDragAndDropAllCorrectCount(int count)
    {
        dragAndDrop.SetDragAndDropAllCorrectCount(count);
    }

    public void DragAndDropAnswerCorrect()
    {
        dragAndDrop.DragAndDropAnswerCorrect();
    }

    public void CheckAllRotationIsCorrect(List<float> rotations)
    {
        rotationGame.CheckAllRotationCorrect(rotations);
    }

    public void SetSpotDifferenceAllCorrectCount(int count)
    {
        spotDifference.SetSpotDifferenceAllCorrectCount(count);
    }

    public void SpotDifferenceCorrect()
    {
        spotDifference.SpotDifferenceAnswerCorrect();
    }

    public void SetTicTacToeTile(int row, int col, int element) 
    {
        ticTacToe.SetTile(row, col, element);
    }
}