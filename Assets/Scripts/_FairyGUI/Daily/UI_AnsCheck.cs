/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Daily
{
    public partial class UI_AnsCheck : GButton
    {
        public GLoader m_SpotCheck;
        public Transition m_CheckAns;
        public const string URL = "ui://16q0hed8ndjqt2t";

        public static UI_AnsCheck CreateInstance()
        {
            return (UI_AnsCheck)UIPackage.CreateObject("Daily", "AnsCheck");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_SpotCheck = (GLoader)GetChildAt(0);
            m_CheckAns = GetTransitionAt(0);
        }
    }
}