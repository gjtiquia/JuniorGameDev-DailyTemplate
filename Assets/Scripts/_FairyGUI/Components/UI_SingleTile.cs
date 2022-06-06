using FairyGUI;
using FairyGUI.Utils;
using UnityEngine;

namespace Daily
{
    public partial class UI_SingleTile : GComponent
    {
        public bool _active;
        public int _number;
        public bool _newlyFormed;
        public Vector2 _boardPosition;

        public UI_SingleTile()
        {
            _boardPosition = new Vector2(-1, -1);
        }

        public bool IsNewlyFormed()
        {
            return _newlyFormed;
        }
    }
}