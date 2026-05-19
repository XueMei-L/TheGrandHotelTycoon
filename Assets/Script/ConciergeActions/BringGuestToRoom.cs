using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BringGuestToRoom : GAction
{
    public override bool PrePerform()
    {
        Debug.Log($"【系统检查】BringGuestToRoom - 开始查验行李员身后的客人...");

        // 1. 用安全局部变量搜寻，绝对不污染基类 target
        GameObject guest = null;

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.GetComponent<GAgent>() != null && child.name != "BaseCharacter")
            {
                guest = child; 
                break;
            }
        }

        // 2. 【核心拦截】身后没人，直接流产！绝对不往下走
        if (guest == null)
        {
            Debug.LogWarning("【逻辑拦截】行李员身后没有跟着客人，拒绝执行去房间动作。");
            this.target = null; 
            return false; 
        }

        // 3. 既然能走到这里，guest 绝对安全存在，直接拿钥匙
        GAgent guestAgent = guest.GetComponent<GAgent>();
        GameObject assignedRoom = guestAgent.inventory.FindItemWithTag("Room");
        
        if (assignedRoom != null)
        {
            // 4. 【高能喂食】把房间正式赋值给基类的 target！
            this.target = assignedRoom; 
            Debug.Log($"【行李员】精准识别到了客人 {guest.name}，正在带往专属房间：{assignedRoom.name}");
            return true;
        }
        else
        {
            Debug.LogError($"【错误】客人 {guest.name} 包里没有房间钥匙！");
            this.target = null;
            return false;
        }
    }

    public override bool PostPerform()
    {
        // GameObject guest = target;

        // // 精准抓取真正的客人
        // for (int i = 0; i < transform.childCount; i++)
        // {
        //     GameObject child = transform.GetChild(i).gameObject;
        //     if (child.GetComponent<GAgent>() != null && child.name != "BaseCharacter")
        //     {
        //         guest = child;
        //         break;
        //     }
        // }
        
        GameObject guest = target;
        Debug.Log("target", guest);
        

        if (guest != null)
        {
            // 解绑
            guest.transform.SetParent(null); 
            Debug.Log("已经解绑", target);

            // 还原客人双腿
            NavMeshAgent guestAgent = guest.GetComponent<NavMeshAgent>();
            if (guestAgent != null) guestAgent.enabled = true; 

            GAgent gAgent = guest.GetComponent<GAgent>();
            gAgent.beliefs.ModifyState("inRoom", 1);
            
            Debug.Log($"【行李员成功】已到达房间，解绑并重新激活 {guest.name} 的寻路组件。");
        }
        
        // 解开行李员个人的上一轮执念枷锁
        GWorld.Instance.GetWorld().ModifyState("isClientInRoom", 1);
        beliefs.ModifyState("hasPickedUpGuest", 0);
        beliefs.ModifyState("temproomEscorted", 0); // 🚨 改为0，避免在发呆打转
        beliefs.ModifyState("lookAround", 0);       // 🚨 顺手激活巡逻欲望

        // 🚨【核心修改：复用现有的 clientWaiting 逻辑】
        // 查找场景里所有叫 "Client" 标签的物体
        
        GameObject[] remainingGuests = GameObject.FindGameObjectsWithTag("Client");
        int realWaitingCount = 0;

        foreach (var g in remainingGuests)
        {
            GAgent agent = g.GetComponent<GAgent>();
            // 只有身上还没有 "inRoom" 信念的，才是真正还在大厅苦苦排队的客人！
            if (agent != null && !agent.beliefs.HasState("inRoom"))
            {
                realWaitingCount++;
            }
        }

        // 根据点名结果，精准控盘 clientWaiting
        if (realWaitingCount == 0)
        {
            beliefs.ModifyState("clientWaiting", 0); // 🚨 没人等了！更新状态为 0
            Debug.Log("【大厅盘点】行李员看了一眼大厅：已经空无一人，待会儿可以去巡逻了。");
        }
        else
        {
            beliefs.ModifyState("clientWaiting", 1); // 🚨 还有人等！更新状态为 1
            Debug.Log($"【大厅盘点】行李员看了一眼大厅：还有 {realWaitingCount} 个人在排队！不能摸鱼。");
        }
        
        // 5. 动作彻底做完，把肉体目标归零
        this.target = null; 
        return true;
    }
}