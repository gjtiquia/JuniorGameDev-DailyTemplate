using System.Text;
using Daily;
using FairyGUI;
using UnityEngine;
using Easy.MessageHub;
using System.Collections.Generic;
using System.Diagnostics;
using System;

public class DailyViewLogic : ViewLogic
{
    public DragAndDropSubViewLogic dragAndDrop;
    public RotationGameSubViewLogic rotationGame;
    public SpotDifferenceSubViewLogic spotDifference;
    public TicTacToeSubViewLogic ticTacToe;
    public TileGameSubViewLogic tileGame;

    DailySystemLogic systemLogic;
    APIManager apiManager;
    UI_Page uiView; public Dictionary<string, List<string>> components = new Dictionary<string, List<string>>();

    public DailyViewLogic(ISystemLogic systemLogic, IMessageHub hub, APIManager apiManager, string packageName, string viewName) :
    base(systemLogic, hub, packageName, viewName)
    {
        this.systemLogic = systemLogic as DailySystemLogic;
        this.apiManager = apiManager;
    }

    public override void CreateView(Dictionary<string, object> viewInfo = null)
    {
        base.CreateView();

        // Setup sub logics
        uiView = view.GetUI() as UI_Page;
        dragAndDrop = new DragAndDropSubViewLogic(uiView, this.systemLogic);
        rotationGame = new RotationGameSubViewLogic(uiView, this.systemLogic);
        spotDifference = new SpotDifferenceSubViewLogic(uiView, this.systemLogic);
        ticTacToe = new TicTacToeSubViewLogic(uiView, this.systemLogic);
        tileGame = new TileGameSubViewLogic(uiView, this.systemLogic);

        // Subscrible event
        //tokenList.Add(messageHub.Subscribe<ContinueEvent>(OnContinueEvent));
        tokenList.Add(messageHub.Subscribe<DragAndDropAllCorrect>(dragAndDrop.OnDragAndDropAllCorrect));
        tokenList.Add(messageHub.Subscribe<SpotDifferenceAllCorrect>(spotDifference.OnSpotDifferenceAllCorrect));
        tokenList.Add(messageHub.Subscribe<RotationAllCorrect>(rotationGame.OnRotationAllCorrect));
        tokenList.Add(messageHub.Subscribe<TicTacToeGameOver>(ticTacToe.OnGameOver));
        tokenList.Add(messageHub.Subscribe<TileGameEnd>(tileGame.OnGameEnd));

        // Setup mini game
        systemLogic.SetDragAndDropAllCorrectCount(dragAndDrop.SetupDraggable());
        systemLogic.SetSpotDifferenceAllCorrectCount(spotDifference.SetupSpotDifference());
        rotationGame.SetupRotationGame();
        ticTacToe.SetupGame();
        tileGame.SetupTiles();
    }
}
