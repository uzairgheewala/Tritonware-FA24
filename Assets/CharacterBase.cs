using UnityEngine;
using System.Collections.Generic;

public class CharacterBase : ScriptableObject {
    public string characterName;
    public List<Trait> traits;
    public List<Motive> motives;
    public List<ItemBase> inventory;
    public Relationship relationships;
    public bool isKiller;
    public Sprite characterSprite;
}

public class Trait
{
    public string traitName;
    public string description;
}

public class Motive
{
    public string motiveDescription;
    public int intensity; 
}

public class Relationship
{
    public Dictionary<string, int> relationshipLevels; // Key: CharacterName, Value: Relationship Score
}
