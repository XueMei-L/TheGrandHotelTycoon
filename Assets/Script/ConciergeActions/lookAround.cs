using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAround : GAction
{
    public override bool PrePerform()
    {
        GameObject[] publicAreas = GameObject.FindGameObjectsWithTag("KitechenPublicArea");

        if (publicAreas != null && publicAreas.Length > 0)
        {
            int randomIndex = Random.Range(0, publicAreas.Length);
            
            this.target= publicAreas[randomIndex];

            return true;
        }
        return false; 
    }

    public override bool PostPerform()
    {
        beliefs.ModifyState("lookAround", 1); 
        return true;
    }
}