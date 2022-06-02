/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Daily
{
    public partial class UI_DragItem : GButton
    {
        public GGraph m_bg;
        public const string URL = "ui://16q0hed88dw9c";

        public static UI_DragItem CreateInstance()
        {
            return (UI_DragItem)UIPackage.CreateObject("Daily", "DragItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = (GGraph)GetChildAt(0);
        }
    }
}