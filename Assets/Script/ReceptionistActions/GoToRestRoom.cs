// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class GoToRestRoom : GAction
// {
//     public override bool PrePerform()
//     {
//         return true;
//     }

//     public override bool PostPerform()
//     {
//         // 到了休息室，前台获得“正在休息”的个人信念
//         beliefs.ModifyState("isResting", 1);
//         Debug.Log("【前台】现在没有客人，我先去休息室摸鱼了。");
//         return true;
//     }
// }