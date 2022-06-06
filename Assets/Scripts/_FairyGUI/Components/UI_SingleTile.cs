using FairyGUI;
using FairyGUI.Utils;
using UnityEngine;

namespace Daily
{
    public partial class UI_SingleTile : GComponent
    {
        public bool active;
        public int number;
        public Vector2 boardPosition;

        public UI_SingleTile()
        {
            boardPosition = new Vector2(-1, -1);
        }
    }
}