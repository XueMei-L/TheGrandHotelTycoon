using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject clientPrefab;
    
    private int maxRoomsInHotel = 0; 

    void Start()
    {
        GameObject[] allRooms = GameObject.FindGameObjectsWithTag("Room");
        maxRoomsInHotel = allRooms.Length;

        Debug.Log($"[Spawn] Hotel has {maxRoomsInHotel} rooms. The guest limit will be based on this!");

        // cada 5-15 s, intentar generar un nuevo cliente
        Invoke("SpawnClientLoop", 5f);
    }

    void SpawnClientLoop()
    {
        GameObject[] currentClients = GameObject.FindGameObjectsWithTag("Client");

        Debug.Log($"[Spawn] Current number of clients in scene: {currentClients.Length} / Total hotel rooms: {maxRoomsInHotel}");

        if (currentClients.Length < maxRoomsInHotel)
        {
            Instantiate(clientPrefab, this.transform.position, Quaternion.identity);
            Debug.Log($"[Spawn] Room not full, successfully generated a new client!");
        }
        else
        {
            Debug.Log($"[Spawn] Hotel rooms are full (current: {currentClients.Length}/total: {maxRoomsInHotel}), no more clients will be generated.");
        }

        float nextSpawnTime = Random.Range(5f, 15f);
        Invoke("SpawnClientLoop", nextSpawnTime);
    }
}