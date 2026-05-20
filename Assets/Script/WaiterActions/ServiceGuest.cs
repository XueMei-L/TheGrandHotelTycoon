using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceGuest : GAction
{

    public override bool PrePerform()
    {
        // 1. 满世界寻找有客人在上面坐着、且等待服务的椅子
        // 我们可以通过在场景里找所有 RestaurantChair 组件
        GameObject[] serviceAreas = GameObject.FindGameObjectsWithTag("ServiceArea");
        
        int randomIndex = Random.Range(0, serviceAreas.Length);
        target = serviceAreas[randomIndex];

        
        Debug.Log($"【服务员】发现 客人， 正前往其对应的服务区 {target.name}");
        return true;
    }

    public override bool PostPerform()
    {
        // 服务完了，给客人打个勾
        beliefs.ModifyState("serviceGuest", 1);
        Debug.Log($"【服务员】成功服务了 客人，完成目标 ServiceGuest");
        return true;
    }

}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class ServiceGuest : GAction
// {
//     public override bool PrePerform()
//     {
//         // 🌟 精准数值判定：只要数字降到 0 或以下，服务员直接拒绝空跑！

//         // 🌟 2. 智能寻找真正有客人的服务区
//         GameObject[] serviceAreas = GameObject.FindGameObjectsWithTag("ServiceArea");
//         target = serviceAreas[Random.Range(0, serviceAreas.Length)];
//         Debug.Log($"【服务员】发现有客人的区域，正前往：{target.name}");
//         return true;
//     }

//     public override bool PostPerform()
//     {
//         // 服务完扣减账本
//         // GWorld.Instance.GetWorld().ModifyState("clientWaiting", -1);
//         beliefs.ModifyState("serviceGuest", 1);
//         return true;
//     }
// }