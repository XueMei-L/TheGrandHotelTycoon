using UnityEngine;

public class Sleep : GAction
{
    public override bool PrePerform()
    {
        // 1. 获取当前角色所在的房间
        GameObject myRoom = this.GetComponent<GAgent>().inventory.FindItemWithTag("Room");
        if (myRoom == null) return false;

        // 2. 在房间的子物体中寻找床 (假设你给房间里的床预制体挂了 "Bed" 标签，或者用名字查找)
        Transform bedTransform = myRoom.transform.Find("BedArea"); 
        if (bedTransform != null)
        {
            target = bedTransform.gameObject;
            cost = Random.Range(5f, 10f); 
            duration = Random.Range(1f, 10f); 
            Debug.Log($"【{gameObject.name}】在床上了");
            return true;
        }
        Debug.LogError($"【GOAP错误】在房间 {myRoom.name} 下面，找不到名字叫 \"BedArea\" 的子物体！请检查场景层级！");
        return false;
    }

    public override bool PostPerform()
    {
        Debug.Log($"【{gameObject.name}】执行postperform了");

        beliefs.ModifyState("isRested", 1);
        Debug.Log($"【{gameObject.name}】在床上美美地睡了一觉！");
        return true;
    }
}