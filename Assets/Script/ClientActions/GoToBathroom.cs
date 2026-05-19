using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToBathroom : GAction
{
    public override bool PrePerform()
    {
        Debug.Log($"【客人】{gameObject.name} 准备去洗澡，开始寻找专属浴室...");

        // 1. 🚨【核心】从自己的背包里，精准把前台分配给他的那间房（Room）拿出来
        GameObject myRoom = inventory.FindItemWithTag("Room");

        Debug.Log($"Room is {myRoom} ");

        if (myRoom != null)
        {
            Transform bathroomTransform = myRoom.transform.Find("BathroomArea"); 

            Debug.Log($"我在这里");

            if (bathroomTransform != null)
            {
                Debug.Log($"我找到了厕所");
                
                // 3. 把基类的肉体终点 target 强行指定为我房间里专属的这间浴室！
                this.target = bathroomTransform.gameObject;
                
                Debug.Log($"【精准寻路】{gameObject.name} 成功锁定了自己房间 {myRoom.name} 内的专属洗手间！出发！");
                return true;
            }
        }
        else
        {
            Debug.LogError($"【严重逻辑错误】客人 {gameObject.name} 脑子里想洗澡!但他的背包里根本没有房间钥匙Room!");
        }

        return false; 
    }

    public override bool PostPerform()
    {
        // 洗完澡了，把舒适度打勾或者把洗澡状态打勾
        beliefs.ModifyState("takeAShower", 1); 
        Debug.Log($"【客人】{gameObject.name} 在专属浴室洗香香完毕！");
        return true;
    }
}