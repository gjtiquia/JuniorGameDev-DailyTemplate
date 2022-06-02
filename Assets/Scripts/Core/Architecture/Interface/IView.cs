using FairyGUI;

public interface IView
{
    public void CreateView();
    public void RemoveView();
    public void SetViewVisible(bool isVisible);
    public void FitSize(GObject view);
}