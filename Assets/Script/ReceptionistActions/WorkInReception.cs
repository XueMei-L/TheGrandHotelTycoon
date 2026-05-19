using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkInReception : GAction
{
    private GameObject currentTargetClient = null;

    public override bool PrePerform()
    {
        // 🌟 核心修复 1：出发前，就从排队队列里把这个客人“叫号”叫出来锁死！
        // 这样可以确保前台走过去之后，手里一定有要接待的客人
        Debug.Log("现在有 " + GWorld.Instance.PrintClientList() + " 在排队等候办理入住。");
        currentTargetClient = GWorld.Instance.RemoveClient();
        
        if (currentTargetClient == null)
        {
            // 如果队列空了，说明没人排队，前台就不用走过去了
            return false; 
        }

        return true; 
    }

    public override bool PostPerform()
    {
        // 2. 已经到达柜台，开始为出发前锁定的客人办理入住
        if (currentTargetClient != null)
        {
            // 分配房间 
            GameObject assignedRoom = GWorld.Instance.RemoveRoom(); 
            
            if (assignedRoom != null)
            {
                // 塞进客人的个人背包
                currentTargetClient.GetComponent<GAgent>().inventory.AddItem(assignedRoom);
                GWorld.Instance.GetWorld().ModifyState("freeRoom", -1); // 全局空房数量 -1
                // 直接修改客人的个人信念
                currentTargetClient.GetComponent<GAgent>().beliefs.ModifyState("getRoom", 1);
                
                Debug.Log($"【前台】成功将 {assignedRoom.name} 分配给新生成的客人：{currentTargetClient.name}");
            }
            else
            {
                Debug.LogWarning("【前台】爆满了！没有多余房间发给客人！");
            }
            
            // 全局等待人数 -1
            GWorld.Instance.GetWorld().ModifyState("guestWaitingCheckIn", -1);
        }

        // 🌟 核心修复 2：为了让前台能接待下一个动态生成的客人，
        // 必须在动作彻底结束时，强行把自己的工作标签摘掉！
        // 这样下一帧如果 guestWaitingCheckIn 还大于 0，GOAP 就会逼她再次走这个 Action！
        StartCoroutine(ResetWorkingState());

        return true;
    }

    // 用一个微秒级的协程，在框架结算完 After Effects 之后，把状态拔掉
    IEnumerator ResetWorkingState()
    {
        yield return new WaitForEndOfFrame();
        beliefs.RemoveState("isWorking");
        currentTargetClient = null; // 清空当前接待人
    }
}