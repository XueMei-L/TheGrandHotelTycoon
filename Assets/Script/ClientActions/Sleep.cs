using UnityEngine;

public class Sleep : GAction
{
    public override bool PrePerform()
    {
        GameObject myRoom = this.GetComponent<GAgent>().inventory.FindItemWithTag("Room");
        if (myRoom == null) return false;

        Transform bedTransform = myRoom.transform.Find("BedArea"); 
        if (bedTransform != null)
        {
            target = bedTransform.gameObject;
            cost = Random.Range(5f, 10f); 
            duration = Random.Range(1f, 10f); 
            return true;
        }
        return false;
    }

    public override bool PostPerform()
    {
        beliefs.ModifyState("isRested", 1);
        Debug.Log($"[{gameObject.name}], is sleeping...");
        return true;
    }
}