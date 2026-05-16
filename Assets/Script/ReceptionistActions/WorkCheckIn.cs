using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceivingGuestsToCheckIn : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        GameObject guest = GWorld.Instance.RemoveClient();
        Debug.Log("guest" + guest);
        if (guest != null)
        {
            // 【核心新增】接待完客人后，前台把自己的“正在休息”状态改回 0
            // 这样前台就不会卡在柜台，而是会在办完业务后重新判定：如果还有人，继续接待；没人了，回休息室。
            beliefs.ModifyState("isWorking", 1);
            // 等待五秒
            guest.GetComponent<GAgent>().beliefs.ModifyState("clientHasRegistered", 1);
            GWorld.Instance.GetWorld().ModifyState("guestWaitingCheckIn", -1);
            Debug.Log("【前台】已经从休息室赶来，成功接待了 " + guest.name);
        }
        
        return true;
    }

}