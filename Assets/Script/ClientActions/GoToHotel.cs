using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToHotel : GAction
{
    public override bool PrePerform()
    {
        // // testing
        // if (beliefs.HasState("hasRegisted") || beliefs.HasState("getRoom"))
        // {
        //     return false;
        // }
        return true;
    }

    public override bool PostPerform()
    {
        beliefs.ModifyState("hasArrived", 1);
        beliefs.ModifyState("atHotel", 1);
        Debug.Log($"【{gameObject.name}】has arrived at the hotel.");
        return true;
    }
}