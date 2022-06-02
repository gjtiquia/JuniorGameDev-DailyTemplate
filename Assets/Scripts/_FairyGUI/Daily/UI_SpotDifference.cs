/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Daily
{
    public partial class UI_SpotDifference : GComponent
    {
        public Controller m_Page;
        public GGraph m_bg;
        public GLoader m_LeftImage;
        public GLoader m_RightImage;
        public GProgressBar m_ScoreBar;
        public UI_Spot m_Spot_A_1;
        public UI_Spot m_Spot_A_2;
        public UI_Spot m_Spot_A_3;
        public UI_Spot m_Spot_A_4;
        public UI_Spot m_Spot_A_5;
        public UI_Spot m_Spot_B_1;
        public UI_Spot m_Spot_B_2;
        public UI_Spot m_Spot_B_3;
        public UI_Spot m_Spot_B_4;
        public UI_Spot m_Spot_B_5;
        public GGroup m_Spots;
        public GList m_AnsCheckList;
        public GList m_AnsCheckList_;
        public GTextField m_CompleteText;
        public const string URL = "ui://16q0hed8maqg0";

        public static UI_SpotDifference CreateInstance()
        {
            return (UI_SpotDifference)UIPackage.CreateObject("Daily", "SpotDifference");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_Page = GetControllerAt(0);
            m_bg = (GGraph)GetChildAt(0);
            m_LeftImage = (GLoader)GetChildAt(1);
            m_RightImage = (GLoader)GetChildAt(2);
            m_ScoreBar = (GProgressBar)GetChildAt(3);
            m_Spot_A_1 = (UI_Spot)GetChildAt(4);
            m_Spot_A_2 = (UI_Spot)GetChildAt(5);
            m_Spot_A_3 = (UI_Spot)GetChildAt(6);
            m_Spot_A_4 = (UI_Spot)GetChildAt(7);
            m_Spot_A_5 = (UI_Spot)GetChildAt(8);
            m_Spot_B_1 = (UI_Spot)GetChildAt(9);
            m_Spot_B_2 = (UI_Spot)GetChildAt(10);
            m_Spot_B_3 = (UI_Spot)GetChildAt(11);
            m_Spot_B_4 = (UI_Spot)GetChildAt(12);
            m_Spot_B_5 = (UI_Spot)GetChildAt(13);
            m_Spots = (GGroup)GetChildAt(14);
            m_AnsCheckList = (GList)GetChildAt(15);
            m_AnsCheckList_ = (GList)GetChildAt(16);
            m_CompleteText = (GTextField)GetChildAt(17);
        }
    }
}