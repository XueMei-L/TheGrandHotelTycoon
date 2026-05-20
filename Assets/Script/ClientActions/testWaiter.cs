// using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testWaiter : GAction
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

        GameObject chair = GWorld.Instance.RemoveChair();
        chosenChair = chair;
        if (chair != null)
        {
            beliefs.ModifyState("inRoom", 0);
            
            target = chair;
            Collider chairCollider = chair.GetComponent<Collider>();
            if (chairCollider != null) 
            {
                Debug.Log($"【客人】如果椅子有碰撞体，强行把它变成 Trigger!让客人能百分百走进椅子的中心点！");
                chairCollider.isTrigger = true; 
            }
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