using Easy.MessageHub;

public class DragAndDropSubLogic
{
    DailyData userData;
    IMessageHub messageHub;

    public DragAndDropSubLogic(DailyData userData, IMessageHub hub)
    {
        this.userData = userData;
        this.messageHub = hub;
    }

    public void SetDragAndDropAllCorrectCount(int count)
    {
        userData.dragAndDropData.allCorrectCount = count;
    }

    public void DragAndDropAnswerCorrect()
    {
        userData.dragAndDropData.correctCount++;
        CheckDragAndDropAllAnswerCorrect();
    }

    void CheckDragAndDropAllAnswerCorrect()
    {
        if (userData.dragAndDropData.correctCount >= userData.dragAndDropData.allCorrectCount)
        {
            messageHub.Publish<DragAndDropAllCorrect>(null);
        }
    }
}