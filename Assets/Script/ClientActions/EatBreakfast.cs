// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.AI; // 引入导航

// public class EatBreakfast : GAction
// {

//     public override bool PrePerform()
//     {
//         GameObject restaurant = GameObject.FindWithTag("RestaurantArea");
//         target = restaurant.transform.Find("Chair").gameObject; 
//         Debug.Log($"【客人】睡饱了，现在要去吃早饭");
//         return true;
//     }

//     public override bool PostPerform()
//     {

//         beliefs.ModifyState("eatBreakfast", 1);
//         Debug.Log($"【客人】{gameObject.name} 吃完早饭了，准备离开酒店。");
//         return true;
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatBreakfast : GAction
{
    // 🌟 用一个变量记录这个客人最终锁定的那把椅子，供 PostPerform 释放使用
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
            Debug.Log($"【客人】{gameObject.name} 成功锁定了餐厅里的 {chair.name} 这把椅子！出发！");
            return true;
        }
        else
        {
            Debug.LogError($"【客人】{gameObject.name} 想吃早饭，但餐厅里没有空闲的椅子了！");
            return false;
        }
    }

    public override bool PostPerform()
    {
        // 🌟 5. 吃完饭准备离开，必须老老实实把椅子释放掉，让给下一个动态生成的客人！
        GWorld.Instance.AddChair(chosenChair); // 把椅子还回全局餐厅椅子队列

        // 修改信念，达成目标
        beliefs.ModifyState("eatBreakfast", 1);
        Debug.Log($"【客人】{gameObject.name} 吃完早饭了，已经腾出椅子，准备离开酒店。");
        return true;
    }
}