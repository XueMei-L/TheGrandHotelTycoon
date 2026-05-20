using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackLuggage : GAction
{
    public override bool PrePerform()
    {
        // 1. 从自己的背包里找出当初分配给自己的那个房间
        GameObject myRoom = inventory.FindItemWithTag("Room");
        
        if (myRoom == null)
        {
            Debug.LogError($"【{gameObject.name}】想去打包行李，但是背包里根本没找到房间钥匙(Room)!");
            target = null;
            return false; // 🛑 关键修正：找不到房间，直接返回 false 拒绝执行，防止底层崩溃！
        }

        // 2. 寻找房间内部的衣柜（Closet）子物体
        Transform closetTransform = myRoom.transform.Find("Closet");

        if (closetTransform == null)
        {
            // 🚨 防御提示：如果报这个错，说明你 Room 预制体里的衣柜名字不叫 "Closet"
            Debug.LogError($"【{gameObject.name}】在房间 {myRoom.name} 中找不到名字叫 'Closet' 的子物体！请检查预制体结构！");
            target = null;
            return false; // 🛑 关键修正：找不到衣柜，拦截动作
        }

        // 3. 顺畅锁定目标
        target = closetTransform.gameObject;
        Debug.Log($"【客人】{gameObject.name} 准备前往房间 {myRoom.name} 的衣柜 ({target.name}) 打包行李。");
        return true;
    }

    public override bool PostPerform()
    {
        // 4. 修改自己的信念，达成打包目标\
        GWorld.Instance.GetWorld().ModifyState("clientWaiting", -1);
        beliefs.ModifyState("hasPackedLuggage", 1);
        
        Debug.Log($"【客人】{gameObject.name} 已经打包好行李了，下一步准备去 checkOut 退房。");
        return true;
    }
}