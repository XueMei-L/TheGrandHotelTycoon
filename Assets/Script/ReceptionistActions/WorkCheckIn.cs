using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceivingGuestsToCheckIn : GAction
{
    public override bool PrePerform()
    {
        // target = GWorld.Instance.RemoveClient();
        // Debug.Log("target:" + target);
        // if (target == null)
        //     return false;
        return true;
    }

    public override bool PostPerform()
    {
        // GameObject guest = GWorld.Instance.RemoveClient();
        target = GWorld.Instance.RemoveClient();
        Debug.Log("target:" + target);
        
        if (target != null)
        {
            // 分配房间 asignar habitacion
            GameObject assignedRoom = GWorld.Instance.RemoveRoom(); 
            Debug.Log("room: " + assignedRoom);
            
            if (assignedRoom != null)
            {
                target.GetComponent<GAgent>().inventory.AddItem(assignedRoom);
                Debug.Log("exit inventory.");
            }

            beliefs.ModifyState("isWorking", 1);

            // 🚨【核心新增】由前台亲手解开客人的封印！把客人的 hasRegisted 改成 1！
            target.GetComponent<GAgent>().beliefs.ModifyState("clientHasRoom", 1);

            // 2. 正常的发卡和世界状态修改
            GWorld.Instance.GetWorld().ModifyState("guestWaitingCheckIn", -1);
            Debug.Log("client has finished CheckIn");
        }
        return true;
    }

}