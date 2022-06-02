using FairyGUI;
using UnityEngine;

public class View : IView
{
    GObject fairyUI;

    string package;
    string uiViewName;

    public View(string package, string uiViewName)
    {
        this.package = package;
        this.uiViewName = uiViewName;
    }

    public GObject GetUI()
    {
        return fairyUI;
    }

    public virtual void CreateView()
    {
        fairyUI = UIPackage.CreateObject(package, uiViewName);
        GRoot.inst.AddChild(fairyUI);
        FitSize(fairyUI);
    }

    public virtual void RemoveView()
    {
        GRoot.inst.RemoveChild(fairyUI);
        fairyUI.Dispose();
    }

    public virtual void SetViewVisible(bool isVisible)
    {
        fairyUI.visible = isVisible;
    }

    public void FitSize(GObject view)
    {
        int width = Screen.width;
        int height = Screen.height;

        width = Mathf.CeilToInt(width / UIContentScaler.scaleFactor);
        height = Mathf.CeilToInt(height / UIContentScaler.scaleFactor);
        view.SetSize(width, height);
        view.SetXY(0, 0, true);
    }
}