using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkInReception : GAction
{
    private GameObject currentTargetClient = null;

    public override bool PrePerform()
    {
        currentTargetClient = GWorld.Instance.RemoveClient();
        
        if (currentTargetClient == null)
        {
            return false; 
        }

        return true; 
    }

    public override bool PostPerform()
    {
        if (currentTargetClient != null)
        {
            GameObject assignedRoom = GWorld.Instance.RemoveRoom(); 
            
            if (assignedRoom != null)
            {
                currentTargetClient.GetComponent<GAgent>().inventory.AddItem(assignedRoom);
                GWorld.Instance.GetWorld().ModifyState("freeRoom", -1); // room -1
                currentTargetClient.GetComponent<GAgent>().beliefs.ModifyState("getRoom", 1);
                
                Debug.Log($"[receptionist] give {assignedRoom.name} to {currentTargetClient.name}");
            }
            
            // client -1
            GWorld.Instance.GetWorld().ModifyState("guestWaitingCheckIn", -1);
        }

        StartCoroutine(ResetWorkingState());

        return true;
    }

    IEnumerator ResetWorkingState()
    {
        yield return new WaitForEndOfFrame();
        beliefs.RemoveState("isWorking");
        currentTargetClient = null; // clean client
    }
}