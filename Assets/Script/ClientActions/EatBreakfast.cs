// using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatBreakfast : GAction
{
    private GameObject chosenChair = null;

    public override bool PrePerform()
    {
        GameObject restaurant = GameObject.FindWithTag("RestaurantArea");
        if (restaurant == null)
        {
            Debug.LogError($"【客人】{gameObject.name} 想吃早饭，但餐厅根本没找到！");
            return false;
        }

        GameObject chair = GWorld.Instance.RemoveChair(); // 从全局餐厅椅子队列里拿一把
        chosenChair = chair;
        if (chair != null)
        {
            target = chair; // 把基类的肉体终点 target 强行指定为这把椅子！
            beliefs.ModifyState("inRoom", 0); // 先把之前进房间的状态清掉，才能顺利达成吃早饭的目标
            Debug.Log($"【客人】{gameObject.name} 成功锁定了餐厅里的 {chair.name} 这把椅子！出发！");

            RestaurantChair chairScript = chair.GetComponent<RestaurantChair>();
            if (chairScript != null)
            {
                chairScript.currentGuest = this.gameObject;
            }

            GWorld.Instance.GetWorld().ModifyState("clientWaiting", +1);
            Debug.Log("ClientWaiting");
            Debug.Log($"【客人】{gameObject.name} 锁定了 {chair.name} 并坐下，等待服务员。");
            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool PostPerform()
    {
        Debug.Log($"【客人】{gameObject.name} 吃完早饭了，起立准备离开。");

        // 释放椅子，但注意：此时先不要彻底清空椅子的 currentGuest，留给服务员认人或者清理
        GWorld.Instance.AddChair(chosenChair); 
        
        beliefs.ModifyState("eatBreakfast", 1);
        return true;
    }
}