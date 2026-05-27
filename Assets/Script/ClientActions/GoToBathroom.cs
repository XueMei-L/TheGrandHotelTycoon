using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToBathroom : GAction
{
    public override bool PrePerform()
    {
        Debug.Log($"[Client]{gameObject.name} is going to the bathroom..");

        GameObject myRoom = inventory.FindItemWithTag("Room");

        Debug.Log($"Room is {myRoom} ");

        Transform bathroomTransform = myRoom.transform.Find("BathroomArea"); 
        if (bathroomTransform != null)
        {
            Debug.Log($"find bathroom");
            
            this.target = bathroomTransform.gameObject;
            
            return true;
        }

        return false; 
    }

    public override bool PostPerform()
    {
        beliefs.ModifyState("takeAShower", 1); 
        return true;
    }
}