using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // 引入导航

public class FollowConcierge : GAction
{

    public override bool PrePerform()
    {

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