using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFood : GAction
{
    public override bool PrePerform()
    {
        GameObject getFoodArea = GameObject.FindWithTag("GetFoodArea");
        target = getFoodArea;
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().RemoveState("IsFoodReady");
        return true;
    }
}