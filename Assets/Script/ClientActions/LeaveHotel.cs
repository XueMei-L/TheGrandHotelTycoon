using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveHotel : GAction
{
    public override bool PrePerform()
    {
        GameObject homePoint = GameObject.FindWithTag("Home");

        if (homePoint == null)
        {
            // 这样控制台会直接打印出是哪个小人找不到家，而不是直接弹出红色的崩溃报错
            Debug.LogWarning($"【防卡顿】{gameObject.name} 没能通过 'Home' 标签找到物体，动作紧急拦截！");
            
            target = null; // 确保是空的
            return false;  // 🛑 关键：返回 false！这样框架就知道目的地丢了，根本不会去跑底层的寻路代码，完美避开报错！
        }

        target = homePoint;

        // 🌟 4. 接下来再跑你的退房和日志逻辑
        if (!GWorld.Instance.GetWorld().HasState("getRoom"))
        {
            Debug.Log($"【{gameObject.name}】确认已无房间，开始走向 Home 点：{target.name}!");
            return true; 
        }

        Debug.Log($"【{gameObject.name}】正在走向目的地：{target.name}");
        return true;
    }

    public override bool PostPerform()
    {
        Debug.Log($"【{gameObject.name}】离开酒店了!");
        Debug.Log($"【系统清理】{gameObject.name} 已到家，正在彻底抹除肉体及全部组件。");

        // 🌟 1. 瞬间关闭导航组件，防止它在死后还继续计算寻路路径
        var agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent != null) agent.enabled = false;

        // 🌟 2. 瞬间隐藏，别让渲染占用显卡
        gameObject.SetActive(false);

        // 🌟 3. 彻底从内存中连根拔起
        Destroy(this.gameObject);

        beliefs.ModifyState("LeaveHotel", 1);
        return true;
    }
}

