using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackLuggage : GAction
{
    // 🌟 用一个变量记录这个客人最终锁定的那把椅子，供 PostPerform 释放使用

    public override bool PrePerform()
    {
        GameObject myRoom = this.GetComponent<GAgent>().inventory.FindItemWithTag("Room");
        if (myRoom != null)
        {
            target = myRoom.transform.Find("Closet").gameObject;
            
            return true;
        }
        Debug.LogError($"【{gameObject.name}】想去房间，但是背包里根本没找到房间钥匙(Room)!");

        return true;
    }

    public override bool PostPerform()
    {

        // 修改信念，达成目标
        beliefs.ModifyState("clientWaiting", -1);
        beliefs.ModifyState("hasPackedLuggage", 1);
        Debug.Log($"【客人】{gameObject.name} 打包好行李了,准备去checkOut。");
        return true;
    }
}