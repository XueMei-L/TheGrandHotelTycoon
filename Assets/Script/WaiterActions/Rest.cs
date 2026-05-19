using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waiterRest : GAction
{

    public override bool PrePerform()
    {
        GameObject restArea = GameObject.FindWithTag("RestArea");
        if (restArea == null)
        {
            Debug.LogError("没有找到休息区！");
            return false;
        }
        Debug.Log($"【服务员】感觉累了，准备去休息区 {restArea.name} 休息一下...");
        target = restArea;
        return true;
    }

    public override bool PostPerform()
    {

        beliefs.ModifyState("isRested", 1);
        Debug.Log($"【服务员】休息好了，完成目标 isRested");
        return true;
    }

}