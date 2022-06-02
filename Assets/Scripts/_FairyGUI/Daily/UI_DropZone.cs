/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Daily
{
    public partial class UI_DropZone : GComponent
    {
        public Controller m_DropType;
        public Controller m_IsCorrect;
        public GLoader m_icon;
        public GTextField m_title;
        public Transition m_Correct;
        public const string URL = "ui://16q0hed88dw9d";

        public static UI_DropZone CreateInstance()
        {
            return (UI_DropZone)UIPackage.CreateObject("Daily", "DropZone");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_DropType = GetControllerAt(0);
            m_IsCorrect = GetControllerAt(1);
            m_icon = (GLoader)GetChildAt(1);
            m_title = (GTextField)GetChildAt(2);
            m_Correct = GetTransitionAt(0);
        }
    }
}