using UnityEngine;

public class ClueBase : ScriptableObject
{
    public string clueName;
    public string description;
    public Sprite clueSprite;
    public string sceneName; // The scene where the clue is located
    public string associatedCharacter; // Character(s) related to the clue
    public int suspicionIncrease;
    public bool isAssignedToKiller = false;
    public bool isDiscovered = false;
}