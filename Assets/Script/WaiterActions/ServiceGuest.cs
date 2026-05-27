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
        
        return true;
    }

    public override bool PostPerform()
    {
        beliefs.ModifyState("serviceGuest", 1);
        return true;
    }

}