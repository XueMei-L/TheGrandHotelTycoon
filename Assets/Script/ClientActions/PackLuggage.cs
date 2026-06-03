using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackLuggage : GAction
{
    public override bool PrePerform()
    {
        GameObject myRoom = inventory.FindItemWithTag("Room");
        
        if (myRoom == null)
        {
            target = null;
            return false;
        }

        Transform closetTransform = myRoom.transform.Find("Closet");

        if (closetTransform == null)
        {
            target = null;
            return false;
        }

        target = closetTransform.gameObject;
        return true;
    }

    public override bool PostPerform()
    {
        // test
        // GWorld.Instance.GetWorld().ModifyState("clientWaiting", -1);
        beliefs.ModifyState("hasPackedLuggage", 1);
        
        Debug.Log($"[Client]{gameObject.name} go check out");
        return true;
    }
}