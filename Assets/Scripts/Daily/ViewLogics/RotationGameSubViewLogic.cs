using FairyGUI;
using System.Collections.Generic;
using Daily;
using UnityEngine;

public class RotationGameSubViewLogic
{
    UI_Page view;
    DailySystemLogic systemLogic;

    UI_RotationGame rotationGame = null;
    bool isRotationComplete = false;

    public RotationGameSubViewLogic(UI_Page view, DailySystemLogic systemLogic)
    {
        this.view = view;
        this.systemLogic = systemLogic;
    }

    public void SetupRotationGame()
    {
        rotationGame = view.GetChild("RotationGame") as UI_RotationGame;
        if (rotationGame == null) return;
        rotationGame.m_ClickToStartButton.onClick.Add(ClickToStart);
        isRotationComplete = false;
    }

    void ClickToStart()
    {
        if (rotationGame != null)
        {
            foreach (var child in rotationGame.GetChildren())
            {
                if (child is UI_Puzzle)
                {
                    child.TweenRotate(Random.Range(1, 4) * 90f, 0.5f).OnComplete(PuzzleInitialSetupComplete);
                    child.asCom.touchable = false;
                }
            }
        }
        rotationGame.GetTransition("StartGame")?.Play();
    }

    public void PuzzleOnClick(EventContext context)
    {
        if (isRotationComplete) return;

        var obj = context.sender as GObject;
        obj.touchable = false;
        obj.TweenRotate(obj.rotation + 90f, 0.25f).OnComplete(PuzzleRotationComplete);
    }

    void PuzzleInitialSetupComplete(GTweener tweener)
    {
        var obj = (GObject)tweener.target;
        obj.touchable = true;
        obj.onClick.Add(PuzzleOnClick);
    }

    void PuzzleRotationComplete(GTweener tweener)
    {
        var obj = (GObject)tweener.target;
        obj.touchable = true;

        var rotList = new List<float>();
        if (rotationGame != null)
        {
            foreach (var child in rotationGame.GetChildren())
            {
                if (child.name.Split('-')[0].Equals("Puzzle"))
                {
                    rotList.Add(child.rotation);
                }
            }
        }
        systemLogic.CheckAllRotationIsCorrect(rotList);
    }

    public void OnRotationAllCorrect(RotationAllCorrect obj)
    {
        isRotationComplete = true;
        var transition = view.GetTransition("MiniGameCorrect");
        if (transition != null)
        {
            transition.Play(() =>
            {
                var index = view.m_Page.selectedIndex;
                // view.m_Page.selectedIndex = (index + 1);
                view.m_Page.selectedIndex = (index + 2); // skip ending page (Page 4) and skip to TicTacToe (Page 5)
            }
            );
        }
        else
        {
            var index = view.m_Page.selectedIndex;
            // view.m_Page.selectedIndex = (index + 1);
            view.m_Page.selectedIndex = (index + 2); // skip ending page (Page 4) and skip to TicTacToe (Page 5)
        }
    }
}