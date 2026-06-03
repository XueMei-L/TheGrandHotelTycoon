using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : GAgent
{
    new void Start()
    {
        base.Start();

        SubGoal s1 = new SubGoal("serviceGuest", 1, false);
        goals.Add(s1, 3);
        
        SubGoal s2 = new SubGoal("getFood", 1, false);
        goals.Add(s2, 5);

    }
    
}