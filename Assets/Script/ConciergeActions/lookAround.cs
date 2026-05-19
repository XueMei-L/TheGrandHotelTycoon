using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAround : GAction
{
    public override bool PrePerform()
    {
        // 1. 🚨【核心】利用 FindGameObjectsWithTag 抓取全场景所有打上 "publicArea" 标签的物体
        GameObject[] publicAreas = GameObject.FindGameObjectsWithTag("KitechenPublicArea");

        // 2. 防崩溃安全检查：确保你至少在场景里放了一个巡逻点
        if (publicAreas != null && publicAreas.Length > 0)
        {
            // 3. 🚨【核心】在 0 到 数组长度 之间随机抽签一个索引
            int randomIndex = Random.Range(0, publicAreas.Length);
            
            // 4. 将抽到的随机物体，正式赋值给当前动作的终点目标（this.target）
            this.target= publicAreas[randomIndex];

            Debug.Log($"【行李员巡逻】大厅现在没活干，我要去巡逻点：{this.target.name} 看看。");
            return true; // 成功找到目的地，允许出发！
        }
        return false; 
    }

    public override bool PostPerform()
    {
        // 巡逻完当前点，在信念库里打勾
        beliefs.ModifyState("lookAround", 1); 
        Debug.Log("【行李员】当前区域巡逻完毕，一切正常。");
        return true;
    }
}