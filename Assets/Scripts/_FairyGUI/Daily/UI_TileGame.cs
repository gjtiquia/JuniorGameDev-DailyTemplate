/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Daily
{
    public partial class UI_TileGame : GComponent
    {
        public GImage m_bg;
        public UI_SingleTile m_Tile_1;
        public UI_SingleTile m_Tile_2;
        public UI_SingleTile m_Tile_3;
        public UI_SingleTile m_Tile_4;
        public UI_SingleTile m_Tile_5;
        public UI_SingleTile m_Tile_6;
        public UI_SingleTile m_Tile_7;
        public UI_SingleTile m_Tile_8;
        public UI_SingleTile m_Tile_9;
        public UI_SingleTile m_Tile_10;
        public UI_SingleTile m_Tile_11;
        public UI_SingleTile m_Tile_12;
        public UI_SingleTile m_Tile_13;
        public UI_SingleTile m_Tile_14;
        public UI_SingleTile m_Tile_15;
        public UI_SingleTile m_Tile_16;
        public const string URL = "ui://16q0hed8ebwat38";

        public static UI_TileGame CreateInstance()
        {
            return (UI_TileGame)UIPackage.CreateObject("Daily", "TileGame");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = (GImage)GetChildAt(0);
            m_Tile_1 = (UI_SingleTile)GetChildAt(1);
            m_Tile_2 = (UI_SingleTile)GetChildAt(2);
            m_Tile_3 = (UI_SingleTile)GetChildAt(3);
            m_Tile_4 = (UI_SingleTile)GetChildAt(4);
            m_Tile_5 = (UI_SingleTile)GetChildAt(5);
            m_Tile_6 = (UI_SingleTile)GetChildAt(6);
            m_Tile_7 = (UI_SingleTile)GetChildAt(7);
            m_Tile_8 = (UI_SingleTile)GetChildAt(8);
            m_Tile_9 = (UI_SingleTile)GetChildAt(9);
            m_Tile_10 = (UI_SingleTile)GetChildAt(10);
            m_Tile_11 = (UI_SingleTile)GetChildAt(11);
            m_Tile_12 = (UI_SingleTile)GetChildAt(12);
            m_Tile_13 = (UI_SingleTile)GetChildAt(13);
            m_Tile_14 = (UI_SingleTile)GetChildAt(14);
            m_Tile_15 = (UI_SingleTile)GetChildAt(15);
            m_Tile_16 = (UI_SingleTile)GetChildAt(16);
        }
    }
}