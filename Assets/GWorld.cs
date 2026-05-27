// Patient Version
using System.Collections.Generic;
using UnityEngine;

public sealed class GWorld
{
    private static readonly GWorld instance = new GWorld();
    private static WorldStates world;
    
    private static Queue<GameObject> clients;
    private static List<GameObject> rooms;

    private static List<GameObject> restaurantChairs;

    static GWorld()
    {
        world = new WorldStates();
        clients = new Queue<GameObject>();
        rooms = new List<GameObject>();
        restaurantChairs = new List<GameObject>();

        GameObject[] rm = GameObject.FindGameObjectsWithTag("Room");
        foreach (GameObject r in rm)
        {
            rooms.Add(r);
        }
            
        if (rm.Length >= 0)
        {
            world.ModifyState("freeRoom", rm.Length);
        }

        GameObject[] chairs = GameObject.FindGameObjectsWithTag("RestaurantChair");
        foreach (GameObject chair in chairs)
        {
            restaurantChairs.Add(chair);
        }

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
        Debug.Log($"[GWorld] Current number of waiting clients: {clients.Count}");
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