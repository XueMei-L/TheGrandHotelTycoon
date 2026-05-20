// using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOut : GAction
{

    public override bool PrePerform()
    {
        GameObject reception = GameObject.FindWithTag("Reception");
        if (reception != null)
        {
            target = reception;
        }
        else
        {
            Debug.LogWarning($"【防卡顿】{gameObject.name} 没能通过 'Reception' 标签找到物体，动作紧急拦截！");
            target = null; 
            return false;  
        }

        return true;
    }

    public override bool PostPerform()
    {
        GameObject myRoom = inventory.FindItemWithTag("Room");
        if (myRoom != null)
        {
            // 1. 如果不是 null，说明包里有！直接顺手把它从包里扔掉
            inventory.RemoveItem(myRoom); 
            
            // 2. 还给整个酒店世界大池子
            GWorld.Instance.AddRoom(myRoom); 
            GWorld.Instance.GetWorld().ModifyState("freeRoom", 1);
            Debug.Log($"【前台退房】{gameObject.name} 成功将房间 {myRoom.name} 归还给酒店。");
        }
        else
        {
            // 3. 如果是 null，说明包里根本没这玩意
            Debug.LogWarning($"【退房异常】{gameObject.name} 来退房，但翻遍背包也没找到 'Room' 标签的物体！");
        }

        // 🌟 2. 清理个人状态和修改世界状态
        beliefs.ModifyState("hasCheckedOut", 1); // 登记退房成功
        beliefs.RemoveState("getRoom");          // 自身不再拥有房间

        Debug.Log($"【{gameObject.name}】在前台办完退房手续了，现在无房一身轻！");
        return true;
    }
}