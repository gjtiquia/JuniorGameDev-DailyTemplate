/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Daily
{
    public partial class UI_SingleTile : GComponent
    {
        public Controller m_Value;
        public GLoader m_loader;
        public Transition m_Show;
        public const string URL = "ui://16q0hed8ebwat3a";

        public static UI_SingleTile CreateInstance()
        {
            return (UI_SingleTile)UIPackage.CreateObject("Daily", "SingleTile");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_Value = GetControllerAt(0);
            m_loader = (GLoader)GetChildAt(0);
            m_Show = GetTransitionAt(0);
        }
    }
}