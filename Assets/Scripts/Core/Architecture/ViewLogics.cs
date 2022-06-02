using Easy.MessageHub;
using System;
using System.Collections.Generic;

public abstract class ViewLogic : IViewLogic
{
    protected View view;
    protected IMessageHub messageHub;
    protected List<Guid> tokenList = new List<Guid>();

    public ViewLogic(ISystemLogic logic, IMessageHub hub, string packageName, string viewName)
    {
        messageHub = hub;
        view = new View(packageName, viewName);
    }

    public virtual void CreateView(Dictionary<string, object> viewInfo = null)
    {
        view.CreateView();
    }

    public virtual void RemoveView()
    {
        for (int i = 0; i < tokenList.Count; i++)
        {
            messageHub.Unsubscribe(tokenList[i]);
        }
        view.RemoveView();
    }

    public virtual void SetViewVisible(bool isVisible)
    {
        view.SetViewVisible(isVisible);
    }
}