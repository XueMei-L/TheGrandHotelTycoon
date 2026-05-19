using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToRoom : GAction
{
    public override bool PrePerform()
    {
        GameObject myRoom = this.GetComponent<GAgent>().inventory.FindItemWithTag("Room");
        if (myRoom != null)
        {
            target = myRoom;
            return true;
        }
        Debug.LogError($"【{gameObject.name}】想去房间，但是背包里根本没找到房间钥匙(Room)!");
        return false;
    }

    public override bool PostPerform()
    {
        // 成功进入房间，达成终极目标
        // beliefs.ModifyState("goToRoom", 1);
        beliefs.ModifyState("inRoom", 1);
        
        Debug.Log($"【{gameObject.name}】已经成功进入房间：{target.name}。");
        return true;
    }
}

