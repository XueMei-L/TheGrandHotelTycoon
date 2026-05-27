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
            Debug.Log($"[{gameObject.name}], found the bathroom, ready to take a shower.");
            target = showerTransform.gameObject;
            cost = Random.Range(1f, 3f); 
            duration = Random.Range(5f, 15f); 
            return true;
        }
        return false;
    }

    public override bool PostPerform()
    {
        beliefs.ModifyState("takeAShower", 1);
        beliefs.ModifyState("inRoom", 1);
        Debug.Log($"[{gameObject.name}], has finished taking a shower.");
        return true;
    }
}