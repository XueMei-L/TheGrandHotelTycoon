// Patient Version
using System.Collections.Generic;
using UnityEngine;

public sealed class GWorld
{
    // 单例模式：确保全局只有一个调度室
    private static readonly GWorld instance = new GWorld();
    private static WorldStates world;
    
    // 酒店核心队列：客人排队队列、空闲房间队列
    private static Queue<GameObject> clients;
    // private static Queue<GameObject> rooms;
    private static List<GameObject> rooms;
    // private static Queue<GameObject> restaurantChairs;

    private static List<GameObject> restaurantChairs;

    static GWorld()
    {
        world = new WorldStates();
        clients = new Queue<GameObject>();
        rooms = new List<GameObject>();
        restaurantChairs = new List<GameObject>();

        // 自动寻找场景中所有带有 "Room" Tag 的房间物体，并塞进空闲房间队列
        GameObject[] rm = GameObject.FindGameObjectsWithTag("Room");
        foreach (GameObject r in rm)
        {
            rooms.Add(r);
        }
            
        // 如果场景里有房间，初始化世界状态中的空闲房间数量
        if (rm.Length > 0)
        {
            world.ModifyState("freeRoom", rm.Length);
        }

        GameObject[] chairs = GameObject.FindGameObjectsWithTag("RestaurantChair");
        foreach (GameObject chair in chairs)
        {
            restaurantChairs.Add(chair);
        }

        Debug.Log($"【GWorld】场景里一共有 {chairs.Length} 把餐厅椅子。");


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
        Debug.Log("当前排队等候的客人有：" + clients.Count);
        return null;
    }

    // --- 房间队列管理 ---
    public void AddRoom(GameObject p)
    {
        rooms.Add(p);
    }

    public int GetRoomCount()
    {
        return rooms.Count;
    }

    public GameObject RemoveRoom()
    {
        if (rooms.Count == 0) return null;
        int randomIndex = Random.Range(0, rooms.Count);
        GameObject room = rooms[randomIndex];
        rooms.RemoveAt(randomIndex);
        return room;
    }

    public GameObject RemoveChair()
    {
        if (restaurantChairs.Count == 0) return null;
        int randomIndex = Random.Range(0, restaurantChairs.Count);
        GameObject chair = restaurantChairs[randomIndex];
        restaurantChairs.RemoveAt(randomIndex);
        return chair;
    }
    
    public void AddChair(GameObject c)
    {
        restaurantChairs.Add(c);
    }

}