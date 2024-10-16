using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewRoom", menuName = "Manor/Room")]
public class RoomData : ScriptableObject
{
    public string roomName;
    public Vector3Int startPosition; // Bottom-left corner
    public int width;
    public int height;
    public Dialogue roomDialogue;
}