using Easy.MessageHub;
using System.Collections.Generic;
using UnityEngine;

public class RotationGameSubLogic
{
    DailyData userData;
    IMessageHub messageHub;

    public RotationGameSubLogic(DailyData userData, IMessageHub hub)
    {
        this.userData = userData;
        this.messageHub = hub;
    }

    public void CheckAllRotationCorrect(List<float> rotations)
    {
        foreach (var rot in rotations)
        {
            if (rot != 0 && rot % 360 != 0) return;
        }
        messageHub.Publish<RotationAllCorrect>(null);
    }
}