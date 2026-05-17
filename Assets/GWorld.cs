// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public sealed class GWorld
// {
//     private static readonly GWorld instance = new GWorld();
//     private static WorldStates world;
//     private static Queue<GameObject> clients;
//     private static Queue<GameObject> rooms;
//     // private static Queue<GameObject> patients;
//     // private static Queue<GameObject> cubicles;

//     static GWorld()
//     {
//         world = new WorldStates();
//         clients = new Queue<GameObject>();
//         rooms = new Queue<GameObject>();

//         clients = new Queue<GameObject>();
//         // find with target room
//         GameObject[] rm = GameObject.FindGameObjectsWithTag("Room");
//         foreach (GameObject r in rm)
//             rooms.Enqueue(r);
            
//         if (rm.Length > 0)
//             world.ModifyState("freeRoom", rm.Length);

//         Time.timeScale = 5; // 加快游戏运行速度
//     }

//     private GWorld()
//     {
//     }

//     // 3. 核心：正确管理【客人】的加入和离开
//     public void AddClient(GameObject p)
//     {
//         clients.Enqueue(p); // 👍 确保进入的是 clients 队列
//     }

//     public GameObject RemoveClient()
//     {
//         if (clients.Count == 0) return null;
//         return clients.Dequeue(); // 👍 前台或服务员可以正确叫号了
//     }

//     // 4. 管理【房间】的借还
//     public void AddRoom(GameObject p)
//     {
//         rooms.Enqueue(p);
//     }

//     public GameObject RemoveRoom()
//     {
//         if (rooms.Count == 0) return null;
//         return rooms.Dequeue();
//     }

//     public static GWorld Instance
//     {
//         get { return instance; }
//     }

//     public WorldStates GetWorld()
//     {
//         return world;
//     }
// }


// Patient Version
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GWorld
{
    // 单例模式：确保全局只有一个调度室
    private static readonly GWorld instance = new GWorld();
    private static WorldStates world;
    
    // 酒店核心队列：客人排队队列、空闲房间队列
    private static Queue<GameObject> clients;
    private static Queue<GameObject> rooms;

    static GWorld()
    {
        world = new WorldStates();
        clients = new Queue<GameObject>();
        rooms = new Queue<GameObject>();

        // 自动寻找场景中所有带有 "Room" Tag 的房间物体，并塞进空闲房间队列
        GameObject[] rm = GameObject.FindGameObjectsWithTag("Room");
        foreach (GameObject r in rm)
        {
            rooms.Enqueue(r);
        }
            
        // 如果场景里有房间，初始化世界状态中的空闲房间数量
        if (rm.Length > 0)
        {
            world.ModifyState("freeRoom", rm.Length);
        }

        // 老师框架自带：将游戏时间加速 5 倍，方便观察 AI 走动
        Time.timeScale = 5; 
    }

    private GWorld() { }

    public static GWorld Instance
    {
        get { return instance; }
    }

    public WorldStates GetWorld()
    {
        return world;
    }

    // --- 客人队列管理 ---
    public void AddClient(GameObject p)
    {
        clients.Enqueue(p);
    }

    public GameObject RemoveClient()
    {
        if (clients.Count == 0) return null;
        return clients.Dequeue();
    }

    public GameObject PrintClientList()
    {
        if (clients.Count == 0) return null;
        foreach (GameObject c in clients)
        {
            Debug.Log(c);
        }
        return clients.Dequeue();
    }

    // --- 房间队列管理 ---
    public void AddRoom(GameObject p)
    {
        rooms.Enqueue(p);
    }

    public GameObject RemoveRoom()
    {
        if (rooms.Count == 0) return null;
        return rooms.Dequeue();
    }
}