using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waiterRest : GAction
{

    public override bool PrePerform()
    {
        GameObject restArea = GameObject.FindWithTag("RestArea");
        target = restArea;
        return true;
    }

    public override bool PostPerform()
    {
        beliefs.RemoveState("getTired");
        return true;
    }

}