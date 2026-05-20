// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class UpdateWorld : MonoBehaviour
// {
//     public Text states;
//     // Start is called before the first frame update
    
//     // Update is called once per frame
//     void LateUpdate()
//     {
//         Dictionary<string, int> worldstates = GWorld.Instance.GetWorld().GetStates();
//         states.text = "";
//         foreach(KeyValuePair<string,int> s in worldstates)
//         {
//             states.text += s.Key + ", " + s.Value + "\n";
//         }

//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateWorld : MonoBehaviour
{
    public Text states;
    
    void LateUpdate()
    {
        Dictionary<string, int> worldstates = GWorld.Instance.GetWorld().GetStates();
        states.text = "";
        
        // 1. 正常遍历字典，打印还大于 0 的状态
        foreach(KeyValuePair<string,int> s in worldstates)
        {
            states.text += s.Key + ", " + s.Value + "\n";
        }

        // 🌟 核心修改点（UI 表现层外挂）：
        // 如果底层字典里把 freeRoom 删了，说明此时绝对是 0 间空房！UI 强行在文本末尾画上 0
        if (!worldstates.ContainsKey("freeRoom"))
        {
            states.text += "freeRoom, 0\n";
        }
        
        // 如果底层字典里把 clientWaiting 删了，说明此时绝对是 0 人等待！UI 强行在文本末尾画上 0
        if (!worldstates.ContainsKey("clientWaiting"))
        {
            states.text += "clientWaiting, 0\n";
        }
    }
}