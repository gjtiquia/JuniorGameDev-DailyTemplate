using FairyGUI;

public class ScoreBar
{
    GProgressBar scoreBar;
    double maxScore;
    double currentScore;

    public ScoreBar(GProgressBar bar, int max)
    {
        scoreBar = bar;
        maxScore = max;
        currentScore = 0;

        scoreBar.max = maxScore;
        scoreBar.min = 0;
        scoreBar.value = 0;
    }

    public double GetCurrentScore()
    {
        return currentScore;
    }

    public void ChangeScore(double value)
    {
        var finalValue = currentScore + value;
        finalValue = finalValue < scoreBar.min ? 0 : finalValue;
        finalValue = finalValue > scoreBar.max ? scoreBar.max : finalValue;
        currentScore = finalValue;

        scoreBar.TweenValue(currentScore, 0.2f);
    }
}