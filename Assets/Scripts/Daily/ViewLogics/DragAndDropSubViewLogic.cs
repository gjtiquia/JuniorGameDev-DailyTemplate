using FairyGUI;
using UnityEngine;
using Daily;

public class DragAndDropSubViewLogic
{
    UI_Page view;
    DailySystemLogic systemLogic;

    GObject dragSource;
    GButton currentDraggingItem;

    public DragAndDropSubViewLogic(UI_Page view, DailySystemLogic systemLogic)
    {
        this.view = view;
        this.systemLogic = systemLogic;
    }

    void OnDragStart(EventContext obj)
    {
        var source = (GButton)obj.sender;
        dragSource = source;
        dragSource.visible = false;

        // Create a copy of the source
        currentDraggingItem = UI_DragItem.CreateInstance().asButton;
        view.AddChild(currentDraggingItem);
        currentDraggingItem.icon = source.icon;
        currentDraggingItem.title = source.title;
        currentDraggingItem.name = source.name;

        // Set the copy to current touch position
        Vector2 currentPos;
        currentPos.x = obj.inputEvent.position.x / UIContentScaler.scaleFactor - dragSource.width / 2;
        currentPos.y = obj.inputEvent.position.y / UIContentScaler.scaleFactor - dragSource.height / 2;
        currentDraggingItem.position = currentPos;

        // Prevent the source from dragging
        obj.PreventDefault();

        // Will start dragging the copy instead of the source
        currentDraggingItem.onDragEnd.Add(OnDragEnd);
        currentDraggingItem.StartDrag((int)obj.data);
    }

    public void OnDragEnd(EventContext obj)
    {
        var dragAndDrop = view.GetChild("DragAndDrop") as UI_DragAndDrop;
        if (dragAndDrop == null) return;

        var dragItem = (GComponent)obj.sender;
        var index = int.Parse(dragItem.name);
        var target = dragAndDrop.GetChild($"Drop-{index}");

        if (!IsOverlap(dragItem, target))
        {
            // Dispose the dragItem as it is a copy of the source
            view.RemoveChild(dragItem);
            dragItem.Dispose();

            dragSource.visible = true;
            dragSource = null;
            return;
        }

        UpdateDropZone(target, dragItem);
        systemLogic.DragAndDropAnswerCorrect();

        view.RemoveChild(dragItem);
        dragItem.Dispose();
        dragSource = null;
    }

    public void OnDragAndDropAllCorrect(DragAndDropAllCorrect obj)
    {
        var transition = view.GetTransition("MiniGameCorrect");
        if (transition != null)
        {
            transition.Play(() =>
            {
                var index = view.m_Page.selectedIndex;
                view.m_Page.selectedIndex = (index + 1);
            }
            );
        }
        else
        {
            var index = view.m_Page.selectedIndex;
            view.m_Page.selectedIndex = (index + 1);
        }
    }

    public int SetupDraggable()
    {
        var dragAndDrop = view.GetChild("DragAndDrop") as UI_DragAndDrop;
        if (dragAndDrop == null) return 0;

        var list = dragAndDrop.m_DragItemList;
        for (int i = 0; i < list.numChildren; i++)
        {
            var item = list.GetChildAt(i).asCom;
            item.draggable = true;
            item.onDragStart.Add(OnDragStart);
        }
        return list.numChildren;
    }

    void UpdateDropZone(GObject target, GComponent dragItem)
    {
        var dropZone = target as UI_DropZone;
        dropZone.m_IsCorrect.SetSelectedIndex(1);
        dropZone.m_title.text = dragItem.text;
        dragItem.visible = false;
    }

    static bool IsOverlap(GObject a, GObject b)
    {
        Rect rectA = new Rect();
        Rect rectB = new Rect();
        if (a != null)
        {

            Vector2 screenPosA = a.LocalToGlobal(Vector2.zero);
            rectA = new Rect(screenPosA.x / UIContentScaler.scaleFactor,
                                    screenPosA.y / UIContentScaler.scaleFactor,
                                    a.size.x,
                                    a.size.y);

            // Debug graph
            // GGraph rectAGraph = new GGraph();
            // rectAGraph.SetSize(a.size.x, a.size.y);
            // rectAGraph.DrawRect(a.size.x, a.size.y, 10, Color.red, Color.white);
            // GRoot.inst.AddChild(rectAGraph);
            // rectAGraph.SetPosition(screenPosA.x / UIContentScaler.scaleFactor, screenPosA.y / UIContentScaler.scaleFactor, 0);
        }

        if (b != null)
        {
            Vector2 screenPosB = b.LocalToGlobal(Vector2.zero);
            rectB = new Rect(screenPosB.x / UIContentScaler.scaleFactor,
                                    screenPosB.y / UIContentScaler.scaleFactor,
                                    b.size.x,
                                    b.size.y);

            // Debug graph
            // GGraph rectBGraph = new GGraph();
            // rectBGraph.SetSize(b.size.x, b.size.y);
            // rectBGraph.DrawRect(b.size.x, b.size.y, 10, Color.green, Color.white);
            // GRoot.inst.AddChild(rectBGraph);
            // rectBGraph.SetPosition(screenPosB.x / UIContentScaler.scaleFactor, screenPosB.y / UIContentScaler.scaleFactor, 0);
        }

        if (a != null && b != null)
        {
            return rectA.Overlaps(rectB);
        }
        return false;
    }
}