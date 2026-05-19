using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // 引入导航

public class PickUpGuest : GAction
{
    public override bool PrePerform()
    {
        target = GWorld.Instance.RemoveClient();
        if (target == null)
            return false;

        GameObject guestsRoom = target.GetComponent<GAgent>().inventory.FindItemWithTag("Room");
        if (guestsRoom != null) {
        }
        GWorld.Instance.GetWorld().ModifyState("freeRoom", -1);
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("clientWaiting", -1);

        if (target != null)
        {
            GAgent clientGAgent = target.GetComponent<GAgent>();
            
            // GameObject guestsRoom = clientGAgent.inventory.FindItemWithTag("Room");
            
            clientGAgent.beliefs.ModifyState("conciergeArrived", 1);
            
        }
        
        beliefs.ModifyState("hasPickedUpGuest", 1);
        beliefs.ModifyState("clientWaiting", 1);
        
        return true;
    }
}