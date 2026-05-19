using UnityEngine;

public class TakeAShower : GAction
{
    public override bool PrePerform()
    {
        // 1. 获取当前角色所在的房间
        GameObject myRoom = this.GetComponent<GAgent>().inventory.FindItemWithTag("Room");
        if (myRoom == null) return false;

        // 2. 在房间的子物体中寻找淋浴间 (假设你给房间里的浴室预制体挂了 "Shower" 标签，或者用名字查找)
        Transform showerTransform = myRoom.transform.Find("BathTub"); 

        if (showerTransform != null)
        {
            Debug.Log($"【{gameObject.name}】找到了浴室，准备洗澡了！");
            target = showerTransform.gameObject;
            
            // 【高级玩法】如果你想让客人随机选择，可以在这里动态随机扰动 Cost
            // cost = Random.Range(1f, 5f); 
            duration = Random.Range(5f, 15f); 
            return true;
        }
        Debug.LogError($"【GOAP错误】在房间 {myRoom.name} 下面，找不到名字叫 \"BathTub\" 的子物体！请检查场景层级！");
        return false;
    }

    public override bool PostPerform()
    {
        // beliefs.ModifyState("isComfortable", 1);
        beliefs.ModifyState("takeAShower", 1);
        beliefs.ModifyState("inRoom", 1);
        Debug.Log($"【{gameObject.name}】舒服地洗了个澡！");
        return true;
    }
}