using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : GAgent
{
    // 使用 new void Start() 覆盖基类的 Start 方法
    new void Start()
    {
        base.Start();

        SubGoal s1 = new SubGoal("hasRegisted", 1, true);
        goals.Add(s1, 5);
        
        SubGoal s2 = new SubGoal("goToRoom", 1, true);
        goals.Add(s2, 1); 
        
        SubGoal s3 = new SubGoal("isSitting", 1, true);
        goals.Add(s3, 4);

        SubGoal s4 = new SubGoal("takeAShower", 1, true);
        goals.Add(s4, 4);
        
        SubGoal s5 = new SubGoal("isRested", 1, true);
        goals.Add(s5, 3);

        SubGoal s6 = new SubGoal("eatBreakfast", 1, true);
        goals.Add(s6, 3);

        SubGoal s7 = new SubGoal("hasPackedLuggage", 1, true);
        goals.Add(s7, 3);

        SubGoal s10 = new SubGoal("hasCheckOut", 1, true);
        goals.Add(s10, 3);

        SubGoal s11 = new SubGoal("leaveHotel", 1, true);
        goals.Add(s11, 3);

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

