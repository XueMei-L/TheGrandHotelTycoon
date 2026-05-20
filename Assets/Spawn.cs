// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Spawn : MonoBehaviour
// {
//     [Header("客人预制体（记得在面板上拖入！）")]
//     public GameObject clientPrefab;
    
//     [Header("游戏刚开始时立刻生成的客人数量")]
//     public int numClients = 2; 

//     void Start()
//     {
//         Invoke("SpawnClientLoop", 5f);
//     }

//     void SpawnClientLoop()
//     {
//         GameObject[] currentClients = GameObject.FindGameObjectsWithTag("Client");

//         Debug.Log($"【生成器】当前场景内客人数量：{currentClients.Length} / numClients");

//         // cuando sea menor que numClients, generar un nuevo cliente
//         if (currentClients.Length < numClients)
//         {
//             Instantiate(clientPrefab, this.transform.position, Quaternion.identity);
//             Debug.Log("【生成器】成功生成了一名新客人！");
//         }
//         else
//         {
//             Debug.Log($"【生成器】酒店人满了,已经达到{numClients}人了, 本次暂不生成。");
//         }

//         // llama otra vez a la funcion
//         float nextSpawnTime = Random.Range(5f, 15f);
//         Invoke("SpawnClientLoop", nextSpawnTime);
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [Header("客人预制体（记得在面板上拖入！）")]
    public GameObject clientPrefab;
    
    // 🌟 核心改动：我们不再需要手动填 numClients 了，改由代码自动去数场景里的房间
    private int maxRoomsInHotel = 0; 

    void Start()
    {
        // 🌟 1. 游戏刚开始时，通过 Tag 算一下整个酒店一共有多少间房，作为我们的“人数上限”
        GameObject[] allRooms = GameObject.FindGameObjectsWithTag("Room");
        maxRoomsInHotel = allRooms.Length;

        Debug.Log($"【生成器初始化】侦测到酒店总共有 {maxRoomsInHotel} 间客房。人数上限将以此为准！");

        // 5秒后开始第一次生成循环
        Invoke("SpawnClientLoop", 5f);
    }

    void SpawnClientLoop()
    {
        // 🌟 2. 算一下目前场景里一共有多少个活着的客人
        GameObject[] currentClients = GameObject.FindGameObjectsWithTag("Client");

        Debug.Log($"【生成器】当前场景内客人数量：{currentClients.Length} / 酒店总房间数：{maxRoomsInHotel}");

        // 🌟 3. 核心逻辑修改：如果【当前人数】小于【酒店总房间数】，就继续生成！
        if (currentClients.Length < maxRoomsInHotel)
        {
            Instantiate(clientPrefab, this.transform.position, Quaternion.identity);
            Debug.Log("【生成器】房间还没满，成功生成了一名新客人！");
        }
        else
        {
            // 当人数等于或大于房间总数时，触发拦截
            Debug.Log($"【生成器】酒店房间全满了（当前{currentClients.Length}人/共{maxRoomsInHotel}房）, 本次暂不生成。");
        }

        // llama otra vez a la funcion (继续随机循环调用自身)
        float nextSpawnTime = Random.Range(5f, 15f);
        Invoke("SpawnClientLoop", nextSpawnTime);
    }
}