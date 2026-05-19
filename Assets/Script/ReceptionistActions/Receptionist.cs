using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receptionist : GAgent
{
    new void Start()
    {
        base.Start();

        SubGoal s1 = new SubGoal("isWorking", 1, false);
        goals.Add(s1, 3);
        
    }
}