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
        return false;
    }

    public override bool PostPerform()
    {
        // beliefs.ModifyState("isComfortable", 1);
        beliefs.ModifyState("isSitting", 1);
        beliefs.ModifyState("inRoom", 1);
        Debug.Log($"[{gameObject.name}], sitdown in the room");
        return true;
    }
}