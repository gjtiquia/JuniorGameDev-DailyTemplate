/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Daily
{
    public partial class UI_Tile : GButton
    {
        public Controller m_state;
        public GLoader m_loader;
        public const string URL = "ui://16q0hed8ebwat32";

        public static UI_Tile CreateInstance()
        {
            return (UI_Tile)UIPackage.CreateObject("Daily", "Tile");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_loader = (GLoader)GetChildAt(0);
        }
    }
}