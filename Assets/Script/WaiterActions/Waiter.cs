using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : GAgent
{
    new void Start()
    {
        base.Start();

        // clientWaitingFood
        SubGoal s1 = new SubGoal("serviceGuest", 1, false);
        goals.Add(s1, 4);
        
        SubGoal s2 = new SubGoal("isRested", 1, false);
        goals.Add(s2, 1);

    }
    
}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Waiter : GAgent
// {
//     new void Start()
//     {
//         base.Start();

//         // 优先目标 1：服务客人（权重 3）
//         SubGoal s1 = new SubGoal("serviceGuest", 1, false);
//         goals.Add(s1, 3);
        
//         // 优先目标 2：去休息（权重 5！比服务客人还要高，一旦累了必须马上执行）
//         SubGoal s2 = new SubGoal("isRested", 1, false);
//         goals.Add(s2, 5);

//         // 🌟 核心驱动：启动定时器，15 到 20 秒后服务员就会变累
//         Invoke("GetTired", 30);
//     }

//     void GetTired()
//     {
//         // 🌟 给自己拍上一个“精疲力竭”的个人信念
//         beliefs.ModifyState("exhausted", 0); 

//         // 重新循环调用，确保以后还会变累
//         Invoke("GetTired", 30);
//         Debug.Log($"【服务员】{gameObject.name} 感觉累了，准备去休息了...");
//     }
// }