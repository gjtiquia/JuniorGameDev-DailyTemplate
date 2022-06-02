/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Daily
{
    public partial class UI_TicTacToe : GComponent
    {
        public UI_Tile m_Tile_1;
        public UI_Tile m_Tile_2;
        public UI_Tile m_Tile_3;
        public UI_Tile m_Tile_4;
        public UI_Tile m_Tile_5;
        public UI_Tile m_Tile_6;
        public UI_Tile m_Tile_7;
        public UI_Tile m_Tile_8;
        public UI_Tile m_Tile_9;
        public const string URL = "ui://16q0hed8ebwat2z";

        public static UI_TicTacToe CreateInstance()
        {
            return (UI_TicTacToe)UIPackage.CreateObject("Daily", "TicTacToe");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_Tile_1 = (UI_Tile)GetChildAt(1);
            m_Tile_2 = (UI_Tile)GetChildAt(2);
            m_Tile_3 = (UI_Tile)GetChildAt(3);
            m_Tile_4 = (UI_Tile)GetChildAt(4);
            m_Tile_5 = (UI_Tile)GetChildAt(5);
            m_Tile_6 = (UI_Tile)GetChildAt(6);
            m_Tile_7 = (UI_Tile)GetChildAt(7);
            m_Tile_8 = (UI_Tile)GetChildAt(8);
            m_Tile_9 = (UI_Tile)GetChildAt(9);
        }
    }
}