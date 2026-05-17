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
        // 触发别人的
        GWorld.Instance.GetWorld().ModifyState("clientWaiting", 1);
        GWorld.Instance.AddClient(this.gameObject);
        
        // 触发自己的
        beliefs.ModifyState("isWaiting", 1);
        return true;
    }
}

