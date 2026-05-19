using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToRoom : GAction
{
    public override bool PrePerform()
    {
        GameObject myRoom = this.GetComponent<GAgent>().inventory.FindItemWithTag("Room");
        if (myRoom != null)
        {
            target = myRoom;
            return true;
        }
        return false;
    }

    public override bool PostPerform()
    {
        beliefs.ModifyState("goToRoom", 1);
        beliefs.ModifyState("inRoom", 1);
        
        Debug.Log($"【{gameObject.name}】go to room: {target.name}。");
        return true;
    }
}

