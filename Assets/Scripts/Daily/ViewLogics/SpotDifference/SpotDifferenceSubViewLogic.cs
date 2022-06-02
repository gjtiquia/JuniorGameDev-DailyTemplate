using FairyGUI;
using Daily;
using UnityEngine;

public class SpotDifferenceSubViewLogic
{
    UI_Page view;
    DailySystemLogic systemLogic;
    private bool[] checkedAns;
    private ScoreBar scoreBar;

    UI_SpotDifference spotDifference;
    //private Controller gameStatusController;

    public SpotDifferenceSubViewLogic(UI_Page view, DailySystemLogic systemLogic)
    {
        this.view = view;
        this.systemLogic = systemLogic;
    }

    public int SetupSpotDifference()
    {
        //Get all spots
        int i = 1;

        if (view.GetChild("SpotDifference") == null) return -1;

        spotDifference = view.GetChild("SpotDifference") as UI_SpotDifference;
        while (spotDifference.GetType().GetField($"m_Spot_A_{i}") != null)
        {
            var spot = spotDifference.GetType().GetField($"m_Spot_A_{i}").GetValue(spotDifference) as GButton;
            spot.onClick.Add(SpotOnClick);

            var bSpot = spotDifference.GetType().GetField($"m_Spot_B_{i}").GetValue(spotDifference) as GButton;
            if (bSpot != null)
            {
                bSpot.onClick.Add(SpotOnClick);
            }

            i++;
        }

        checkedAns = new bool[i - 1];
        scoreBar = new ScoreBar(spotDifference.m_ScoreBar, i - 1);
        return i - 1;
        //gameStatusController = spotDifference.m_Page;
    }

    public void SpotOnClick(EventContext context)
    {
        // Current touching spot
        var spot = context.sender as UI_Spot;
        spot.touchable = false;
        spot.m_Display.Play();

        // Another spot
        Debug.Log(spot.name);
        var nameArray = spot.name.Split('-');
        var anotherSpot = nameArray[1] == "A" ? "B" : "A";
        DisplayAnotherSpot(nameArray, anotherSpot);

        CheckAnswer();
        systemLogic.SpotDifferenceCorrect();
    }

    void DisplayAnotherSpot(string[] nameArray, string target)
    {
        var anotherSpot = spotDifference.GetChild($"{nameArray[0]}-{target}-{nameArray[2]}") as UI_Spot;
        anotherSpot.m_Display.Play();
        anotherSpot.touchable = false;
    }

    public void OnSpotDifferenceAllCorrect(SpotDifferenceAllCorrect obj)
    {
        var transition = view.GetTransition("MiniGameCorrect");
        if (transition != null)
        {
            transition.Play(() =>
            {
                var index = view.m_Page.selectedIndex;
                // view.m_Page.selectedIndex = (index + 1);
                // view.m_Page.selectedIndex = (index + 2); // skip to ending
                view.m_Page.selectedIndex = (index + 3); // skip to TicTacToe
            }
            );
        }
        else
        {
            var index = view.m_Page.selectedIndex;
            // view.m_Page.selectedIndex = (index + 1);
            view.m_Page.selectedIndex = (index + 3); // skip to TicTacToe
        }
    }

    private void CheckAnswer()
    {
        var spotDifference = view.GetChild("SpotDifference") as UI_SpotDifference;
        var ansCheckList = spotDifference.m_AnsCheckList;
        if (ansCheckList != null)
        {
            for (int i = 0; i < checkedAns.Length; i++)
            {
                if (!checkedAns[i])
                {
                    var child = ansCheckList.GetChildAt(i) as GComponent;
                    if (child == null) break;
                    child.GetTransition("CheckAns").Play();
                    checkedAns[i] = true;
                    scoreBar.ChangeScore(1);
                    break;
                }
            }
        }

        //CheckGameStatus();
    }

    // private void CheckGameStatus()
    // {
    //     if(scoreBar.GetCurrentScore()==checkedAns.Length)
    //     {
    //         gameStatusController.selectedIndex = 1;
    //     }
    // }
}