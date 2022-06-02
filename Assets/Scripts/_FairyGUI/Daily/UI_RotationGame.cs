/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Daily
{
    public partial class UI_RotationGame : GComponent
    {
        public Controller m_c1;
        public UI_Puzzle m_Puzzle_0;
        public UI_Puzzle m_Puzzle_1;
        public UI_Puzzle m_Puzzle_2;
        public UI_Puzzle m_Puzzle_3;
        public UI_Puzzle m_Puzzle_4;
        public UI_Puzzle m_Puzzle_5;
        public UI_Puzzle m_Puzzle_6;
        public UI_Puzzle m_Puzzle_7;
        public UI_Puzzle m_Puzzle_8;
        public GGraph m_ClickToStartButton;
        public GTextField m_title;
        public Transition m_StartGame;
        public Transition m_ClickToStart_motion;
        public Transition m_ClickToStart_loop;
        public const string URL = "ui://16q0hed8ox7813";

        public static UI_RotationGame CreateInstance()
        {
            return (UI_RotationGame)UIPackage.CreateObject("Daily", "RotationGame");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_c1 = GetControllerAt(0);
            m_Puzzle_0 = (UI_Puzzle)GetChildAt(0);
            m_Puzzle_1 = (UI_Puzzle)GetChildAt(1);
            m_Puzzle_2 = (UI_Puzzle)GetChildAt(2);
            m_Puzzle_3 = (UI_Puzzle)GetChildAt(3);
            m_Puzzle_4 = (UI_Puzzle)GetChildAt(4);
            m_Puzzle_5 = (UI_Puzzle)GetChildAt(5);
            m_Puzzle_6 = (UI_Puzzle)GetChildAt(6);
            m_Puzzle_7 = (UI_Puzzle)GetChildAt(7);
            m_Puzzle_8 = (UI_Puzzle)GetChildAt(8);
            m_ClickToStartButton = (GGraph)GetChildAt(9);
            m_title = (GTextField)GetChildAt(11);
            m_StartGame = GetTransitionAt(0);
            m_ClickToStart_motion = GetTransitionAt(1);
            m_ClickToStart_loop = GetTransitionAt(2);
        }
    }
}