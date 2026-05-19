using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [Header("客人预制体（记得在面板上拖入！）")]
    public GameObject clientPrefab;
    
    [Header("游戏刚开始时立刻生成的客人数量")]
    public int numClients = 2; 

    void Start()
    {
        Invoke("SpawnClientLoop", 5f);
    }

    void SpawnClientLoop()
    {
        GameObject[] currentClients = GameObject.FindGameObjectsWithTag("Client");

        Debug.Log($"【生成器】当前场景内客人数量：{currentClients.Length} / numClients");

        // cuando sea menor que numClients, generar un nuevo cliente
        if (currentClients.Length < numClients)
        {
            Instantiate(clientPrefab, this.transform.position, Quaternion.identity);
            Debug.Log("【生成器】成功生成了一名新客人！");
        }
        else
        {
            Debug.Log($"【生成器】酒店人满了,已经达到{numClients}人了, 本次暂不生成。");
        }

        // llama otra vez a la funcion
        float nextSpawnTime = Random.Range(5f, 15f);
        Invoke("SpawnClientLoop", nextSpawnTime);
    }
}