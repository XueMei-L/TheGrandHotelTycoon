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
        // other people
        GWorld.Instance.GetWorld().ModifyState("clientWaiting", 1);
        GWorld.Instance.AddClient(this.gameObject);
        
        // self state
        beliefs.ModifyState("isWaiting", 1);
        return true;
    }
}

