using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // 引入导航

public class FollowConcierge : GAction
{

    public override bool PrePerform()
    {

        return true;
    }

    public override bool PostPerform()
    {
        return true;
    }
}