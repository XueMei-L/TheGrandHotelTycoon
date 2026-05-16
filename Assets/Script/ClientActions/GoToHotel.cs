using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToHotel : GAction
{
    public override bool PrePerform()
    {
        return true; // 动作前不需要任何特殊物理判断
    }

    public override bool PostPerform()
    {
        // 动作完成后，在客人自己的“个人信念(beliefs)”里打个勾，记录自己已经到了大门
        beliefs.ModifyState("hasArrived", 1);
        Debug.Log(gameObject.name + " 成功到达大门！");
        return true;
    }
}