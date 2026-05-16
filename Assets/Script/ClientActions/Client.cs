using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : GAgent
{
    // 使用 new void Start() 覆盖基类的 Start 方法
    new void Start()
    {
        // 1. 必须先调用老师基类的初始化，让底下的寻路和规划器准备好
        base.Start();

        // 2. 给客人添加第一个核心目标：想要成功办理入住（clientHasRegisted = 1）
        // 参数：("目标状态名", 目标值[老师框架填1], 达成后是否移除该目标)
        SubGoal s1 = new SubGoal("clientHasRegisted", 1, true);
        goals.Add(s1, 3);
        
        SubGoal s2 = new SubGoal("isWaiting", 1, true);
        goals.Add(s2, 3);
        
        // 3. 把目标加入客人的大脑列表，数字 3 是优先级
    }
}