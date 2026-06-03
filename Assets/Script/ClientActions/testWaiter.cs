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
            Debug.LogError($"[{gameObject.name}] wants to eat breakfast, but the restaurant couldn't be found!");
            return false;
        }

        GameObject chair = GWorld.Instance.RemoveChair();
        chosenChair = chair;
        if (chair != null)
        {
            beliefs.ModifyState("inRoom", 0);
            
            target = chair;
            Collider chairCollider = chair.GetComponent<Collider>();
            chairCollider.isTrigger = true; 

            // RestaurantChair chairScript = chair.GetComponent<RestaurantChair>();
            // if (chairScript != null)
            // {
            //     chairScript.currentGuest = this.gameObject;
            // }

            // GWorld.Instance.GetWorld().ModifyState("clientWaiting", +1);
            // Debug.Log("ClientWaiting");
            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool PostPerform()
    {
        GWorld.Instance.AddChair(chosenChair); 
        
        beliefs.ModifyState("eatBreakfast", 1);
        return true;
    }
}