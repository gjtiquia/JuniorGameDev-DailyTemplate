using FairyGUI;
using UnityEngine;

namespace GComponentExtensionMethods
{
    public static class GComponentExtension
    {
        public static void CheckScrollFreeToCenter(this GComponent gComponent)
        {
            if (gComponent.data == null) return;
            if (gComponent.data.ToString().Contains("scrollfree"))
            {
                if (!gComponent.centered)
                {
                    gComponent.centered = true;
                    Debug.Log("scroll to center");
                    var bg = gComponent.GetChild("bg");
                    gComponent.scrollPane.SetPosX(bg.width / 2 - Screen.width / 2, false);
                    gComponent.scrollPane.SetPosY(bg.height / 2 - Screen.height / 2, false);
                }
            }
        }
    }
}