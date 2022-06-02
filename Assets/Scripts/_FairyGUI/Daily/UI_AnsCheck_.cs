/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Daily
{
    public partial class UI_AnsCheck_ : GComponent
    {
        public GGraph m_SpotCheck;
        public Transition m_CheckAns;
        public const string URL = "ui://16q0hed8yayy7";

        public static UI_AnsCheck_ CreateInstance()
        {
            return (UI_AnsCheck_)UIPackage.CreateObject("Daily", "AnsCheck-");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_SpotCheck = (GGraph)GetChildAt(0);
            m_CheckAns = GetTransitionAt(0);
        }
    }
}