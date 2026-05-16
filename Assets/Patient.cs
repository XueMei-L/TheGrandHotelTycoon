using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : GAgent
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("isWaiting", 1, true);
        goals.Add(s1, 3);

        SubGoal s2 = new SubGoal("isTreated", 1, true);
        goals.Add(s2, 5);

        SubGoal s3 = new SubGoal("isHome", 1, true);
        goals.Add(s3, 5);


        // // 目标 1：客人想要休息（去房间睡觉）
        // // 参数说明: ("状态名字", 目标值, 是否在达成后移除目标)
        // SubGoal s1 = new SubGoal("isRested", 1, true);
        // // 将目标加入客人的目标字典，后面的数字是优先级（优先级越高，AI 越倾向于先完成它）
        // goals.Add(s1, 5);

        // // 目标 2：客人想要吃早餐
        // SubGoal s2 = new SubGoal("hasEaten", 1, true);
        // goals.Add(s2, 3);

        // // 目标 3：终极目标——客人想要打包并成功退房离开酒店
        // // 当这个目标达成后，客人的整套 AI 逻辑就圆满完成了
        // SubGoal s3 = new SubGoal("clientGone", 1, true);
        // goals.Add(s3, 1);
    }

}