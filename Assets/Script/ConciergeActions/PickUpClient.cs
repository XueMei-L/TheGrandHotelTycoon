using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // 引入导航

public class PickUpGuest : GAction
{
    public override bool PrePerform()
    {
        target = GWorld.Instance.RemoveClient();
        if (target == null)
            return false;

        GameObject guestsRoom = target.GetComponent<GAgent>().inventory.FindItemWithTag("Room");
        if (guestsRoom != null) {
            Debug.Log($"【行李员】成功接到客人 {target.name}，得知他已经被前台分配到了：{guestsRoom.name}");
        }
        GWorld.Instance.GetWorld().ModifyState("freeRoom", -1);
        return true;
    }

    public override bool PostPerform()
    {
        // 大厅排队人数减 1
        GWorld.Instance.GetWorld().ModifyState("clientWaiting", -1);

        // 🚨【老师流派核心逻辑】
        // 我们绝对不关客人的 NavMeshAgent，绝对不写 SetParent！
        // 我们只需要像老师那样，把房间（钥匙）塞到客人的背包（inventory）里去！
        if (target != null)
        {
            GAgent clientGAgent = target.GetComponent<GAgent>();
            
            // 假设你在别的地方已经把 Room 塞给客人了，这里做个双保险确保他包里有这把钥匙
            GameObject guestsRoom = clientGAgent.inventory.FindItemWithTag("Room");
            
            // 💡 顺便在客人的信念（beliefs）里打上一个标记，告诉客人的大脑：“行李员来接你了，你可以开始出发去房间了！”
            clientGAgent.beliefs.ModifyState("conciergeArrived", 1);
            
            Debug.Log($"【老师流派】行李员暗中拍了拍 {target.name} 的肩膀，触发客人的回房动作，各自出发！");
        }
        
        // 告诉行李员自己：接到人（任务同步完成）
        beliefs.ModifyState("hasPickedUpGuest", 1);
        beliefs.ModifyState("clientWaiting", 1); // 确保干活期间行李员大厅状态处于忙碌
        
        return true;
    }
}