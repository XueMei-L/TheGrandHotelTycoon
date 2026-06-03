using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef : GAgent
{
    new void Start()
    {
        base.Start();

        SubGoal s1 = new SubGoal("isCooking", 1, false);
        goals.Add(s1, 5);
        
        SubGoal s2 = new SubGoal("prepareFood", 1, false);
        goals.Add(s2, 3);

        Invoke("FoodIsEmpty", 60);
    }
    
    void FoodIsEmpty()
    {
        GWorld.Instance.GetWorld().ModifyState("FoodIsEmpty", 1);
        Invoke("FoodIsEmpty", 60);
        Debug.Log($"[Chef] {gameObject.name} should cook...");
    }

}

