using UnityEngine;

public class TakeAShower : GAction
{
    public override bool PrePerform()
    {
        GameObject myRoom = this.GetComponent<GAgent>().inventory.FindItemWithTag("Room");
        if (myRoom == null) return false;

        Transform showerTransform = myRoom.transform.Find("BathTub"); 

        if (showerTransform != null)
        {
            Debug.Log($"【{gameObject.name}】找到了浴室，准备洗澡了！");
            target = showerTransform.gameObject;
            
            duration = Random.Range(5f, 15f); 
            return true;
        }
        Debug.LogError($"【GOAP错误】在房间 {myRoom.name} 下面，找不到名字叫 \"BathTub\" 的子物体！请检查场景层级！");
        return false;
    }

    public override bool PostPerform()
    {
        beliefs.ModifyState("takeAShower", 1);
        beliefs.ModifyState("inRoom", 1);
        Debug.Log($"【{gameObject.name}】舒服地洗了个澡！");
        return true;
    }
}