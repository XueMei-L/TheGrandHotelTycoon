using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanTable : GAction
{
    private RestaurantChair dirtyChair = null;

    public override bool PrePerform()
    {
        RestaurantChair[] allChairs = FindObjectsByType<RestaurantChair>(FindObjectsSortMode.None);
        
        foreach (RestaurantChair chair in allChairs)
        {
            // 如果椅子放回了池子，但上面的 currentGuest 还没被清空，说明是吃完饭留下的脏桌子
            if (chair.currentGuest != null)
            {
                dirtyChair = chair;
                break;
            }
        }

        if (dirtyChair == null) return false;

        // 走到服务区去擦桌子
        target = dirtyChair.GetServiceArea();
        Debug.Log($"【服务员】发现脏桌子，前往 {target.name} 进行清理");
        return true;
    }

    public override bool PostPerform()
    {
        Debug.Log($"【服务员】清理完毕！{target.name} 现在干净了。");

        // 🌟 彻底抹去椅子上残留的客人信息，代表这张桌椅完全恢复纯净
        if (dirtyChair != null)
        {
            dirtyChair.currentGuest = null; 
        }

        // 世界脏桌子状态 -1
        GWorld.Instance.GetWorld().ModifyState("tableDirty", -1);

        StartCoroutine(ResetCleanState());
        return true;
    }

    IEnumerator ResetCleanState()
    {
        yield return new WaitForEndOfFrame();
        beliefs.RemoveState("CleanTable");
        dirtyChair = null;
    }
}