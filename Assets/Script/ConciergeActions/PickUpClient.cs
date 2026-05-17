using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpGuest : GAction
{
    public override bool PrePerform()
    {
        target = GWorld.Instance.RemoveClient();
        if (target == null)
            return false;

        GameObject guestsRoom = target.GetComponent<GAgent>().inventory.FindItemWithTag("Room");
        if (guestsRoom != null)
            // inventory.AddItem(guestsRoom);
            Debug.Log($"【行李员】成功接到客人 {target.name}，得知他已经被前台分配到了：{guestsRoom.name}");
        else
        {
            GWorld.Instance.AddClient(target);
            target = null;
            return false;
        }
        // 保底机制：如果这个客人居然没被分配房间，把他放回队列，动作取消
        GWorld.Instance.GetWorld().ModifyState("freeRoom", -1);
        return true;
    }

    public override bool PostPerform()
    {
        // if (target)
        //     target.GetComponent<GAgent>().inventory.AddItem(resource);
        GWorld.Instance.GetWorld().ModifyState("clientWaiting", -1);
        
        if (target != null)
        {
            // 3. 让客人变成行李员的子物体，跟着走
            target.transform.SetParent(this.transform);
            target.transform.localPosition = new Vector3(0, 0, -1.5f);
            
            // 激活客人的跟随信念
            target.GetComponent<GAgent>().beliefs.ModifyState("beingEscorted", 1);
            // target.GetComponent<GAgent>().beliefs.ModifyState("inRoom", 1);
        }
        
        // 4. 告诉行李员自己：接到人了
        beliefs.ModifyState("hasPickedUpGuest", 1);
        return true;
    }
}