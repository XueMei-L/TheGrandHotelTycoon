using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [Header("客人预制体（记得在面板上拖入！）")]
    public GameObject clientPrefab;
    
    // [Header("游戏刚开始时立刻生成的客人数量")]
    // public int numClients = 3; 

    void Start()
    {
        // // 1. 游戏一开始，先生成初始数量的客人
        // for (int i = 0; i < numClients; i++)
        // {
        //     // 严谨起见，加个安全判断，防止面板没拖预制体导致游戏崩溃 可以删掉
        //     if (clientPrefab != null)
        //     {
        //         Instantiate(clientPrefab, this.transform.position, Quaternion.identity);
        //     }
        // }

        // 2. 5 秒后开始第一次动态循环生成
        Invoke("SpawnClientLoop", 5f);
    }

    // void Update()
    // {
    //     // 这里不需要写任何东西，生成逻辑完全交给 SpawnClientLoop 和 Invoke 来控制
    //     if (GWorld.Instance.GetRoomCount() == 0)
    //     {
    //         Debug.LogWarning("【生成器警告】当前没有空闲房间了，暂不生成新客人。");
    //         return;
    //     }else{
    //         Debug.Log($"【生成器】当前还有 {GWorld.Instance.GetRoomCount()} 个空闲房间，可以继续生成客人。");
    //         SpawnClientLoop();
    //     }
    // }

    void SpawnClientLoop()
    {
        GameObject[] currentClients = GameObject.FindGameObjectsWithTag("Client");

        Debug.Log($"【生成器】当前场景内客人数量：{currentClients.Length} / 7");

        // cuando sea menor que 7, generar un nuevo cliente
        if (currentClients.Length < 7)
        {
            Instantiate(clientPrefab, this.transform.position, Quaternion.identity);
            Debug.Log("【生成器】成功生成了一名新客人！");
        }
        else
        {
            Debug.Log("【生成器】酒店人满了,已经达到7人了, 本次暂不生成。");
        }

        // llama otra vez a la funcion
        float nextSpawnTime = Random.Range(5f, 15f);
        Invoke("SpawnClientLoop", nextSpawnTime);
    }
}