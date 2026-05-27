// using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOut : GAction
{

    public override bool PrePerform()
    {
        GameObject reception = GameObject.FindWithTag("Reception");
        target = reception;
        return true;
    }

    public override bool PostPerform()
    {
        GameObject myRoom = inventory.FindItemWithTag("Room");
        if (myRoom != null)
        {
            inventory.RemoveItem(myRoom); 
            
            GWorld.Instance.AddRoom(myRoom); 
            GWorld.Instance.GetWorld().ModifyState("freeRoom", 1);
        }
        

        beliefs.ModifyState("hasCheckedOut", 1);
        beliefs.RemoveState("getRoom");

        Debug.Log($"【{gameObject.name}】has checked out.");
        return true;
    }
}