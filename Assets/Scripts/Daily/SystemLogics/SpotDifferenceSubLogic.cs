using Easy.MessageHub;

public class SpotDifferenceSubLogic
{
    DailyData userData;
    IMessageHub messageHub;

    public SpotDifferenceSubLogic(DailyData userData, IMessageHub hub)
    {
        this.userData = userData;
        this.messageHub = hub;
    }

    public void SetSpotDifferenceAllCorrectCount(int count)
    {
        userData.spotDifferenceData.allCorrectCount = count;
    }

    public void SpotDifferenceAnswerCorrect()
    {
        userData.spotDifferenceData.correctCount++;
        CheckSpotDifferenceAllAnswerCorrect();
    }

    void CheckSpotDifferenceAllAnswerCorrect()
    {
        if (userData.spotDifferenceData.correctCount >= userData.spotDifferenceData.allCorrectCount)
        {
            messageHub.Publish<SpotDifferenceAllCorrect>(null);
        }
    }
}
