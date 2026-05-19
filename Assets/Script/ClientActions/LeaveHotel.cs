using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveHotel : GAction
{
    public override bool PrePerform()
    {
        if(!GWorld.Instance.GetWorld().HasState("getRoom"))
        {
            Debug.Log($"【{gameObject.name}】has checed out");
            return true;
        }
        return true;
    }

    public override bool PostPerform()
    {
        Debug.Log($"【{gameObject.name}】has left the hotel, destroy the game object.");
        beliefs.ModifyState("LeaveHotel", 1);
        Destroy(gameObject);
        return true;
    }
}

