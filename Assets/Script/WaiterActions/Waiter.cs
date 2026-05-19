using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : GAgent
{
    new void Start()
    {
        base.Start();

        // clientWaitingFood
        SubGoal s1 = new SubGoal("serviceGuest", 1, false);
        goals.Add(s1, 4);
        
        SubGoal s2 = new SubGoal("isRested", 1, false);
        goals.Add(s2, 2);

    }
    
}

// hasPackedLuggage