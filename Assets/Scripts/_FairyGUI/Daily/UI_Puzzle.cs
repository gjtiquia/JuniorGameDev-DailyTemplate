/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Daily
{
    public partial class UI_Puzzle : GComponent
    {
        public Controller m_Image;
        public GLoader m_Puzzle_0;
        public const string URL = "ui://16q0hed8mwwv1c";

        public static UI_Puzzle CreateInstance()
        {
            return (UI_Puzzle)UIPackage.CreateObject("Daily", "Puzzle");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_Image = GetControllerAt(0);
            m_Puzzle_0 = (GLoader)GetChildAt(0);
        }
    }
}