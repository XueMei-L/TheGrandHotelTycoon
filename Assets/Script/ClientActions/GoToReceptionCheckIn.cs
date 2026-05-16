using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToReceptionCheckIn : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        // 1. 在大厅里登记：让世界状态中“正在前台排队的人数” +1
        GWorld.Instance.GetWorld().ModifyState("guestWaitingCheckIn", 1);
        
        // 2. 把自己这个客人的 GameObject 放进 GWorld 的中央排队队列，等前台来叫号
        GWorld.Instance.AddClient(this.gameObject);
        
        // 3. 在自己的个人信念里打勾：我已经站在前台柜台前了
        beliefs.ModifyState("guestAtCounter", 1);

        Debug.Log(gameObject.name + " 已经站在前台排队，等待接待。");

        return true;
    }
}