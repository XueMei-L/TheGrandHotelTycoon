using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BringGuestToRoom : GAction
{
    public override bool PrePerform()
    {
        GameObject guest = null;

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.GetComponent<GAgent>() != null && child.name != "BaseCharacter")
            {
                guest = child; 
                break;
            }
        }

        if (guest == null)
        {
            this.target = null; 
            return false; 
        }

        GAgent guestAgent = guest.GetComponent<GAgent>();
        GameObject assignedRoom = guestAgent.inventory.FindItemWithTag("Room");
        
        if (assignedRoom != null)
        {
            this.target = assignedRoom; 
            return true;
        }
        else
        {
            this.target = null;
            return false;
        }
    }

    public override bool PostPerform()
    {
        GameObject guest = target;
        Debug.Log("target", guest);
        
        if (guest != null)
        {
            // quit client
            guest.transform.SetParent(null); 
            Debug.Log("已经解绑", target);

            NavMeshAgent guestAgent = guest.GetComponent<NavMeshAgent>();
            if (guestAgent != null) guestAgent.enabled = true; 

            GAgent gAgent = guest.GetComponent<GAgent>();
            gAgent.beliefs.ModifyState("inRoom", 1);
            
        }
        
        GWorld.Instance.GetWorld().ModifyState("isClientInRoom", 1);
        beliefs.ModifyState("hasPickedUpGuest", 0);
        beliefs.ModifyState("temproomEscorted", 0); 
        beliefs.ModifyState("lookAround", 0); 

        
        GameObject[] remainingGuests = GameObject.FindGameObjectsWithTag("Client");
        int realWaitingCount = 0;

        foreach (var g in remainingGuests)
        {
            GAgent agent = g.GetComponent<GAgent>();
            if (agent != null && !agent.beliefs.HasState("inRoom"))
            {
                realWaitingCount++;
            }
        }

        if (realWaitingCount == 0)
        {
            beliefs.ModifyState("clientWaiting", 0);
        }
        else
        {
            beliefs.ModifyState("clientWaiting", 1);
        }
        
        this.target = null; 
        return true;
    }
}