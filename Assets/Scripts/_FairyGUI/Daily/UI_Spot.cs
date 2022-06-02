/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Daily
{
    public partial class UI_Spot : GButton
    {
        public GGraph m_circle;
        public Transition m_Display;
        public const string URL = "ui://16q0hed8yayy5";

        public static UI_Spot CreateInstance()
        {
            return (UI_Spot)UIPackage.CreateObject("Daily", "Spot");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_circle = (GGraph)GetChildAt(0);
            m_Display = GetTransitionAt(0);
        }
    }
}