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
        
        SubGoal s2 = new SubGoal("isRested", 1, false);
        goals.Add(s2, 5);

        // cada 30 s, camarero tiene que descansar
        Invoke("GetTired", 60);
    }

    void GetTired()
    {
        beliefs.ModifyState("getTired", 1);
        Invoke("GetTired", 60);
        Debug.Log($"[Waiter] {gameObject.name} feels tired...");
    }
}