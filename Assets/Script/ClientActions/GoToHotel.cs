using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToHotel : GAction
{
    public override bool PrePerform()
    {
        
        // 如果我已经办完入住（兜里有房间了），我就绝对不再去大门！
        if (beliefs.HasState("hasRegisted") || beliefs.HasState("getRoom"))
        {
            return false; // 严厉拒绝倒车回家
        }
        return true;
    }

    public override bool PostPerform()
    {
        // 记录自己到达了大门
        beliefs.ModifyState("hasArrived", 1);
        beliefs.ModifyState("atHotel", 1);
        Debug.Log($"【{gameObject.name}】成功到达酒店大门！");
        return true;
    }
}