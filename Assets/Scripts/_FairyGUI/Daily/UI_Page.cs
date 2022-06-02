/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Daily
{
    public partial class UI_Page : GComponent
    {
        public Controller m_Page;
        public GButton m_StartButton;
        public GGroup m_Page0;
        public UI_DragAndDrop m_DragAndDrop;
        public GGroup m_Page1;
        public UI_SpotDifference m_SpotDifference;
        public GGroup m_Page2;
        public UI_RotationGame m_RotationGame;
        public GGroup m_Page3;
        public GGroup m_Page4;
        public UI_TicTacToe m_TicTacToe;
        public GTextField m_Title;
        public GGroup m_Page5;
        public GGroup m_CorrectPopup;
        public GGroup m_WrongPopup;
        public GGraph m_block;
        public UI_TileGame m_TileGame;
        public GGroup m_Page6;
        public Transition m_ShowPage1;
        public Transition m_ShowPage2;
        public Transition m_ShowPage3;
        public Transition m_MiniGameCorrect;
        public Transition m_ShowPage4;
        public Transition m_MiniGameWrong;
        public Transition m_ShowPage5;
        public Transition m_ShowPage6;
        public const string URL = "ui://16q0hed8mi8p0";

        public static UI_Page CreateInstance()
        {
            return (UI_Page)UIPackage.CreateObject("Daily", "Page");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_Page = GetControllerAt(0);
            m_StartButton = (GButton)GetChildAt(2);
            m_Page0 = (GGroup)GetChildAt(3);
            m_DragAndDrop = (UI_DragAndDrop)GetChildAt(5);
            m_Page1 = (GGroup)GetChildAt(6);
            m_SpotDifference = (UI_SpotDifference)GetChildAt(9);
            m_Page2 = (GGroup)GetChildAt(10);
            m_RotationGame = (UI_RotationGame)GetChildAt(12);
            m_Page3 = (GGroup)GetChildAt(13);
            m_Page4 = (GGroup)GetChildAt(16);
            m_TicTacToe = (UI_TicTacToe)GetChildAt(18);
            m_Title = (GTextField)GetChildAt(19);
            m_Page5 = (GGroup)GetChildAt(20);
            m_CorrectPopup = (GGroup)GetChildAt(23);
            m_WrongPopup = (GGroup)GetChildAt(26);
            m_block = (GGraph)GetChildAt(27);
            m_TileGame = (UI_TileGame)GetChildAt(28);
            m_Page6 = (GGroup)GetChildAt(29);
            m_ShowPage1 = GetTransitionAt(0);
            m_ShowPage2 = GetTransitionAt(1);
            m_ShowPage3 = GetTransitionAt(2);
            m_MiniGameCorrect = GetTransitionAt(3);
            m_ShowPage4 = GetTransitionAt(4);
            m_MiniGameWrong = GetTransitionAt(5);
            m_ShowPage5 = GetTransitionAt(6);
            m_ShowPage6 = GetTransitionAt(7);
        }
    }
}