using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToBathroom : GAction
{
    public override bool PrePerform()
    {
        GameObject myRoom = inventory.FindItemWithTag("Room");

        Debug.Log($"Room is {myRoom} ");

        if (myRoom != null)
        {
            Transform bathroomTransform = myRoom.transform.Find("BathroomArea"); 

            Debug.Log($"Here");

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