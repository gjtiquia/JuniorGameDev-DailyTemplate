/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Daily
{
    public partial class UI_DragAndDrop : GComponent
    {
        public GList m_DragItemList;
        public UI_DropZone m_Drop_0;
        public UI_DropZone m_Drop_1;
        public UI_DropZone m_Drop_2;
        public UI_DropZone m_Drop_3;
        public UI_DropZone m_Drop_4;
        public UI_DropZone m_Drop_5;
        public const string URL = "ui://16q0hed88dw91";

        public static UI_DragAndDrop CreateInstance()
        {
            return (UI_DragAndDrop)UIPackage.CreateObject("Daily", "DragAndDrop");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_DragItemList = (GList)GetChildAt(0);
            m_Drop_0 = (UI_DropZone)GetChildAt(1);
            m_Drop_1 = (UI_DropZone)GetChildAt(2);
            m_Drop_2 = (UI_DropZone)GetChildAt(3);
            m_Drop_3 = (UI_DropZone)GetChildAt(4);
            m_Drop_4 = (UI_DropZone)GetChildAt(5);
            m_Drop_5 = (UI_DropZone)GetChildAt(6);
        }
    }
}