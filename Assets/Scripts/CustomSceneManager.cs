using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class CustomSceneManager : MonoBehaviour
{
    public RoomData[] rooms; // Assign in Inspector
    public Transform player; // Assign in Inspector

    public RoomConnection[] roomConnections; // Define how rooms are connected

    private string currentRoom;

    public static CustomSceneManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            Logger.LogWarning("Duplicate SceneManager instance destroyed.");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            Logger.Log("SceneManager instance created.");
        }
    }

    void Start()
    {
        if (rooms.Length > 0)
        {
            currentRoom = rooms[0].roomName;
            Logger.Log($"Starting in room: {currentRoom}");
        }

        PersistentCamera.Instance.SetTarget(player);
    }

    void Update()
    {
        // Check for player entering a doorway
        foreach (RoomConnection connection in roomConnections)
        {
            if (currentRoom == connection.fromRoomName)
            {
                Vector3 playerPos = player.position;
                Vector3 doorwayPos = connection.doorwayPosition; // Use world position for simplicity

                // Simple proximity check
                if (Vector3.Distance(playerPos, doorwayPos) < 0.5f)
                {
                    TransitionToRoom(connection.toRoomName, connection.targetEntrancePosition);
                }
            }
        }
    }

    public void TransitionToRoom(string targetRoomName, Vector3Int entrancePosition)
    {
        Logger.Log($"Transitioning from current room: {currentRoom}, to {targetRoomName}");

        // Start the transition process
        StartCoroutine(LoadAndSwitchRoom(targetRoomName, entrancePosition));
    }

    private IEnumerator LoadAndSwitchRoom(string targetRoomName, Vector3 entrancePosition)
    {
        // Load the target room
        Logger.Log($"Loading room: {targetRoomName}");
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(targetRoomName, LoadSceneMode.Single);

        // Wait until the room is loaded
        while (!loadOperation.isDone)
        {
            yield return null;
        }

        Logger.Log($"Room {targetRoomName} loaded successfully.");

        // Update current room
        currentRoom = targetRoomName;

        // Move player to the entrance position in the new room
        player.position = entrancePosition;
        Logger.Log($"Player moved to {entrancePosition}");

        yield return null;
    }

    /*
    private IEnumerator LoadAndSwitchRoom(string targetRoomName, Vector3Int entrancePosition)
    {
        // Load the target room additively
        Logger.Log($"Loading room: {targetRoomName}");
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(targetRoomName, LoadSceneMode.Additive);

        // Wait until the room is loaded
        while (!loadOperation.isDone)
        {
            yield return null;
        }

        Logger.Log($"Room {targetRoomName} loaded successfully.");

        // Optionally, unload the current room
        if (!string.IsNullOrEmpty(currentRoom))
        {
            Logger.Log($"Unloading room: {currentRoom}");
            AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(currentRoom);

            // Wait until the room is unloaded
            while (!unloadOperation.isDone)
            {
                yield return null;
            }

            Logger.Log($"Room {currentRoom} unloaded successfully.");
        }

        // Update current room
        currentRoom = targetRoomName;

        // Move player to the entrance position in the new room
        Vector3 newPlayerPosition = tilemapManager.floorTilemap.CellToWorld(entrancePosition) + new Vector3(0.5f, 0.5f, 0);
        player.position = newPlayerPosition;
        Logger.Log($"Player moved to {newPlayerPosition}");

        yield return null;
    }*/
}

[System.Serializable]
public class RoomConnection
{
    public string fromRoomName;
    public string toRoomName;
    public Vector3Int doorwayPosition; // Position where the player appears in the new room
    public Vector3Int targetEntrancePosition; // Player's entrance position in the target room
}