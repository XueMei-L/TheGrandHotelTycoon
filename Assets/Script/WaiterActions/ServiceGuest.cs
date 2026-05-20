using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceGuest : GAction
{
    public override bool PrePerform()
    {
        GameObject[] serviceAreas = GameObject.FindGameObjectsWithTag("ServiceArea");
        
        int randomIndex = Random.Range(0, serviceAreas.Length);
        target = serviceAreas[randomIndex];

        
        Debug.Log($"【服务员】发现 客人， 正前往其对应的服务区 {target.name}");
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("clientWaiting", -1);
        beliefs.ModifyState("serviceGuest", 1);
        Debug.Log($"【服务员】成功服务了 客人，完成目标 ServiceGuest");
        return true;
    }

}