using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToReceptionCheckIn : GAction
{
    public override bool PrePerform()
    {
        
        // 2. 修改全局状态，通知前台有客人来排队了
        GWorld.Instance.GetWorld().ModifyState("guestWaitingCheckIn", 1);
        GWorld.Instance.AddClient(this.gameObject);
        
        Debug.Log($"【{gameObject.name}】加入排队队列，等待前台接待...");
        return true;
    }

    public override bool PostPerform()
    {
        // 办完业务，打上已登记标签
        beliefs.ModifyState("hasRegisted", 1);
        return true;
    }
}