using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EscortGuestToRoom : GAction
{
    public override bool PrePerform()
    {
        GameObject guest = null;

        // 🚨【精准安全抓取】遍历子物体，只有名字里包含 "Client" 或者身上有 GAgent 的才是真正的客人！
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.GetComponent<GAgent>() != null && child.name != "BaseCharacter")
            {
                guest = child; // 找到了真正的客人！
                break;
            }
        }

        if (guest != null)
        {
            GAgent guestAgent = guest.GetComponent<GAgent>();
            GameObject assignedRoom = guestAgent.inventory.FindItemWithTag("Room");
            
            if (assignedRoom != null)
            {
                this.target = assignedRoom; 
                Debug.Log($"【行李员】精准识别到了客人 {guest.name}，正在带往专属房间：{assignedRoom.name}");
                return true;
            }
            else
            {
                Debug.LogError($"【错误】客人 {guest.name} 包里没有房间钥匙！");
            }
        }
        else
        {
            Debug.LogError("【严重错误】行李员身后根本没有跟着合法的客人智能体！抓到的是空白模型。");
        }
        
        return false; 
    }

    public override bool PostPerform()
    {
        GameObject guest = null;

        // 精准抓取真正的客人
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.GetComponent<GAgent>() != null && child.name != "BaseCharacter")
            {
                guest = child;
                break;
            }
        }
        
        if (guest != null)
        {
            // 3. 释放客人，解除父子级
            guest.transform.SetParent(null); 

            // 4. 🚨【核心新增】还给客人寻路导航能力，方便他自己在房间里活动
            NavMeshAgent guestAgent = guest.GetComponent<NavMeshAgent>();
            if (guestAgent != null)
            {
                guestAgent.enabled = true; // 👈 还原寻路功能
            }

            GAgent gAgent = guest.GetComponent<GAgent>();
            if (gAgent != null)
            {
                gAgent.beliefs.ModifyState("inRoom", 1);
                Debug.Log($"【行李员成功】已到达房间，解绑并重新激活 {guest.name} 的寻路组件。");
            }
        }
        
        beliefs.ModifyState("hasPickedUpGuest", 0);
        beliefs.ModifyState("temproomEscorted", 1);
        return true;
    }
}