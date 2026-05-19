using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToWaitingArea : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("clientWaiting", 1);
        GWorld.Instance.AddClient(this.gameObject);
        
        beliefs.ModifyState("isWaiting", 1);
        return true;
    }
}

