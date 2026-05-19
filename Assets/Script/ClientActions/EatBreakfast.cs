// using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatBreakfast : GAction
{
    private GameObject chosenChair = null;

    public override bool PrePerform()
    {
        GameObject chair = GWorld.Instance.RemoveChair();
        chosenChair = chair;
        if (chair != null)
        {
            target = chair;
            beliefs.ModifyState("inRoom", 0);

            GWorld.Instance.GetWorld().ModifyState("clientWaiting", +1);
            Debug.Log("ClientWaiting");
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