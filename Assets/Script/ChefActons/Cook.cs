using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook : GAction
{
    public override bool PrePerform()
    {
        GameObject cookarea = GameObject.FindWithTag("CookArea");
        target = cookarea;
        return true;
    }

    public override bool PostPerform()
    {
        beliefs.ModifyState("isCooking", 1);
        GWorld.Instance.GetWorld().RemoveState("FoodIsEmpty");
        GWorld.Instance.GetWorld().ModifyState("IsFoodReady", 1);
        return true;
    }
}