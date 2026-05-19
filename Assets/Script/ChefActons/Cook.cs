using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook : GAction
{

    public override bool PrePerform()
    {
        GameObject[] publicAreas = GameObject.FindGameObjectsWithTag("KitechenPublicArea");

        if (publicAreas != null && publicAreas.Length > 0)
        {
            int randomIndex = Random.Range(0, publicAreas.Length);
            
            this.target= publicAreas[randomIndex];

            // Debug.Log($"【厨师】正在烹饪，随机选择。");
            return true;
        }
        return false; 
    }

    public override bool PostPerform()
    {
        // 巡逻完当前点，在信念库里打勾
        beliefs.ModifyState("isCooking", 1); 
        // Debug.Log("【厨师】当前区域烹饪完毕，一切正常。");
        return true;
    }
}