using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveHotel : GAction
{
    public override bool PrePerform()
    {
        if(!GWorld.Instance.GetWorld().HasState("getRoom"))
        {
            Debug.Log($"【{gameObject.name}】没有房间了，无法离开酒店！");
            return false;
        }
        return true;
    }

    public override bool PostPerform()
    {
        beliefs.ModifyState("LeaveHotel", 1);
        return true;
    }
}

