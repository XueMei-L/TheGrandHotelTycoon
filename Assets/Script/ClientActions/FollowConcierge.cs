using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // 引入导航

public class BeEscortedToRoom : GAction
{
    public BeEscortedToRoom()
    {
        // 🚨 靠因果锁被动等待行李员带路，自身不需要走路时间，设为 0
        this.duration = 0f; 
    }

    public override bool PrePerform()
    {
        // 1. 【高能联动】当客人开始执行这个动作时，说明行李员已经在接他了。
        // 为了防止他的寻路组件和行李员冲突，我们在这里把客人的双腿（NavMeshAgent）关掉！
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.enabled = false; // 卸载客人的自主寻路
        }

        Debug.Log($"【客人】{gameObject.name} 已经认可了行李员的带路动作，主动关闭寻路，准备配合被拖走。");
        return true;
    }

    public override bool PostPerform()
    {
        // 2. 动作结束（行李员在后方帮他改了 inRoom=1 信念后，动作自然完结）
        Debug.Log($"【客人】{gameObject.name} 顺利到达房间，动作圆满结束！");
        return true;
    }
}