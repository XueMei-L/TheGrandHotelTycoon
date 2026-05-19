// using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOut : GAction
{

    public override bool PrePerform()
    {
        
        return true;
    }

    public override bool PostPerform()
    {
        beliefs.ModifyState("hasCheckedOut", 1);
        beliefs.RemoveState("getRoom");
        GWorld.Instance.GetWorld().ModifyState("freeRoom", 1);
        GWorld.Instance.AddRoom(target.GetComponent<GAgent>().inventory.FindItemWithTag("Room"));
        return true;
    }
}