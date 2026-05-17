using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToReceptionCheckIn : GAction
{
    public override bool PrePerform()
    {
        GWorld.Instance.AddClient(this.gameObject);
        
        // 🚨【高能警报】此时修改世界状态：大厅排队人数 +1
        GWorld.Instance.GetWorld().ModifyState("guestWaitingCheckIn", 1);
        
        Debug.Log($"【系统】{gameObject.name} 已经到达柜台并加入 GWorld 队列，开始广播通知前台！");
        return true;
    }

    public override bool PostPerform()
    {
        // 3. 在自己的个人信念里打勾：我已经站在前台柜台前了
        beliefs.ModifyState("hasRegisted", 1);
        Debug.Log(gameObject.name + " 前台业务正式办完。");
        return true;
    }
}