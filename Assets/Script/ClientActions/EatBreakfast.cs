// using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatBreakfast : GAction
{
    private GameObject chosenChair = null;

    public override bool PrePerform()
    {
        // asignar una silla al cliente
        GameObject chair = GWorld.Instance.RemoveChair();
        chosenChair = chair;
        if (chair != null)
        {
            beliefs.ModifyState("inRoom", 0);
            
            target = chair;
            // problema de colision
            Collider chairCollider = chair.GetComponent<Collider>();
            if (chairCollider != null) 
            {
                chairCollider.isTrigger = true; 
            }

            RestaurantChair chairScript = chair.GetComponent<RestaurantChair>();
            if (chairScript != null)
            {
                chairScript.currentGuest = this.gameObject;
            }

            // test
            // GWorld.Instance.GetWorld().ModifyState("clientWaiting", +1);
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