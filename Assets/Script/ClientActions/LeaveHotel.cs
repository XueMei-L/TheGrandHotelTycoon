using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveHotel : GAction
{
    public override bool PrePerform()
    {
        GameObject homePoint = GameObject.FindWithTag("Home");

        if (homePoint == null)
        {
            target = null;
            return false;
        }

        target = homePoint;

        if (!GWorld.Instance.GetWorld().HasState("getRoom"))
        {
            return true; 
        }

        return true;
    }

    public override bool PostPerform()
    {
        Debug.Log($"【{gameObject.name}】leving hotel");

        // var agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        // if (agent != null) agent.enabled = false;

        gameObject.SetActive(false);

        Destroy(this.gameObject);

        beliefs.ModifyState("LeaveHotel", 1);
        return true;
    }
}

