using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToReceptionCheckIn : GAction
{
    public override bool PrePerform()
    {
        
        GWorld.Instance.GetWorld().ModifyState("guestWaitingCheckIn", 1);
        GWorld.Instance.AddClient(this.gameObject);
        
        Debug.Log($"【{gameObject.name}】join the queue at the reception desk...");
        return true;
    }

    public override bool PostPerform()
    {
        beliefs.ModifyState("hasRegisted", 1);
        return true;
    }
}