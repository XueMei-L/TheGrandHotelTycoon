using UnityEngine;

public class SitDown : GAction
{
    public override bool PrePerform()
    {
        GameObject myRoom = this.GetComponent<GAgent>().inventory.FindItemWithTag("Room");
        if (myRoom == null) return false;

        Transform chairTransform = myRoom.transform.Find("Chair");
        if (chairTransform != null)
        {
            target = chairTransform.gameObject;
            cost = Random.Range(1f, 5f); 
            duration = Random.Range(1f, 10f); 
            return true;
        }
        Debug.LogError($"【GOAP错误】在房间 {myRoom.name} 下面，找不到名字叫 \"Chair\" 的子物体！请检查场景层级！");
        return false;
    }

    public override bool PostPerform()
    {
        // beliefs.ModifyState("isComfortable", 1);
        beliefs.ModifyState("isSitting", 1);
        beliefs.ModifyState("inRoom", 1);
        Debug.Log($"【{gameObject.name}】坐在椅子上刷了会手机。");
        return true;
    }
}