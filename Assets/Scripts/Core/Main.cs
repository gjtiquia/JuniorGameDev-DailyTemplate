using UnityEngine;
using FairyGUI;
using Daily;
using Easy.MessageHub;
using TMPro;
using System.Diagnostics;

public class Main : MonoBehaviour
{
    public TMP_FontAsset FontAsset;
    public APIManager apiManager;
    public InGameBridge inGameBridge;

    DailySystemLogic dailySystemLogic;

    DailyViewLogic dailyViewLogic;

    IMessageHub messageHub = new MessageHub();

    bool isAuthAlready = false;
    bool GameInitedAlready = false;

    void Start()
    {
        TMPFont font = new TMPFont();
        font.fontAsset = FontAsset;
        font.name = "WenQuanYiMicroHei";
        FontManager.RegisterFont(font);
        UIConfig.defaultFont = font.name;

        _UIBinding();

        InitSystemLogic();
        InitViewLogic();

        ShowFirstView();
    }

    void _UIBinding()
    {
        UIPackage.AddPackage("_FairyGUI/Daily");
        DailyBinder.BindAll();
    }

    void InitSystemLogic()
    {
        dailySystemLogic = new DailySystemLogic(messageHub);
    }

    void InitViewLogic()
    {
        //UnityEngine.Debug.Log($"InitViewLogic:isAuthAlready: {isAuthAlready}");
        dailyViewLogic = new DailyViewLogic(dailySystemLogic, messageHub, apiManager, "Daily", "Page");
    }

    void ShowFirstView()
    {
        dailyViewLogic.CreateView();
    }
}