using System.Collections.Generic;

public interface IViewLogic
{
    public void CreateView(Dictionary<string, object> viewInfo = null);
    public void RemoveView();
    public void SetViewVisible(bool isVisible);
}