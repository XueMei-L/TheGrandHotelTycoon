using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : GAgent
{
    // 使用 new void Start() 覆盖基类的 Start 方法
    new void Start()
    {
        base.Start();

// 🌟 1. 进酒店和登记：一进游戏最迫切的欲望（权重给最高：5）
        // 这样一出生他会绝对优先去走大门、找前台
        SubGoal s1 = new SubGoal("hasRegisted", 1, true);
        goals.Add(s1, 5);
        
        // 🌟 2. 进房间：属于基础长期目标（权重给 1）
        // 只要没进房间，他就会在办完登记后老老实实往房间走
        // SubGoal s2 = new SubGoal("inRoom", 1, false);
        // goals.Add(s2, 1); 
        
        // 🌟 3. 房间内的自由活动（权重给 3，低于登记）
        // 这样在没办完登记前，规划器绝对不会为了洗澡去倒推大门动作！
        SubGoal s3 = new SubGoal("isSitting", 1, true);
        goals.Add(s3, 3);

        SubGoal s4 = new SubGoal("takeAShower", 1, true);
        goals.Add(s4, 3);
        
        SubGoal s5 = new SubGoal("isRested", 1, true);
        goals.Add(s5, 3);

        // 🌟 4. 吃早餐：等一切尘埃落定后再干的事（权重给 2）
        SubGoal s6 = new SubGoal("eatBreakfast", 1, true);
        goals.Add(s6, 3);

        // 🌟 4. 吃早餐：等一切尘埃落定后再干的事（权重给 2）
        SubGoal s7 = new SubGoal("hasPackedLuggage", 1, true);
        goals.Add(s7, 2);

        // 🌟 4. 吃早餐：等一切尘埃落定后再干的事（权重给 2）
        SubGoal s10 = new SubGoal("hasCheckOut", 1, true);
        goals.Add(s10, 2);

        // 🌟 4. 吃早餐：等一切尘埃落定后再干的事（权重给 2）
        SubGoal s11 = new SubGoal("leaveHotel", 1, true);
        goals.Add(s11, 2);

        // StartCoroutine(ComfortLoop());
        
        // SubGoal s2 = new SubGoal("isWaiting", 1, true);
        // goals.Add(s2, 3);

        // SubGoal s3 = new SubGoal("isComfortable", 1, false);
        // goals.Add(s3, 5);
    }
    // IEnumerator ComfortLoop()
    // {
    //     while(true)
    //     {
    //         yield return new WaitForSeconds(5f);
    //         if (beliefs.HasState("isComfortable"))
    //         {
    //             beliefs.RemoveState("isComfortable");
    //             Debug.Log($"【系统】{gameObject.name} 感觉无聊了，想在房间里换个活动...");
    //         }
    //     }
    // }
}

