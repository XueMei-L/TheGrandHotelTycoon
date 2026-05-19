using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Concierge : GAgent
{
    new void Start()
    {
        base.Start();

        // 参数：("目标状态名", 目标值[老师框架填1], 达成后是否移除该目标)
        // SubGoal s1 = new SubGoal("hasPickedUpGuest", 1, false);
        // goals.Add(s1, 5);
        // clientWaiting
        // beliefs.ModifyState("clientWaiting", 0);
        // GWorld.Instance.GetWorld().ModifyState("clientWaiting", 0);

        SubGoal s2 = new SubGoal("temproomEscorted", 1, false);
        goals.Add(s2, 5);
        
        SubGoal s3 = new SubGoal("lookAround", 1, false);
        goals.Add(s3, 2);
        
    }
}