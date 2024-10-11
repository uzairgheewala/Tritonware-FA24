using UnityEngine;
using System.Collections.Generic;

public class CharacterBase : ScriptableObject {

    public string characterName;
    public int age;
    public string role; 
    public Sprite characterSprite; // Default

    public List<Sprite> idleSprites;
    public List<Sprite> walkingSprites; 
    public List<Sprite> interactingSprites; 

    public List<Trait> traits;

    public List<Motive> motives;

    public List<ItemBase> inventory;

    public List<Relationship> relationships;
    public float decayRate; // Default Relationship decay rate

    public bool isKiller = false;
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
    public string characterName; 
    public float relationshipValue; // -100 to 100

    public RelationshipType RelationshipType
    {
        get
        {
            if (relationshipValue >= 50)
                return RelationshipType.Ally;
            else if (relationshipValue <= -50)
                return RelationshipType.Enemy;
            else
                return RelationshipType.Neutral;
        }
    }
}

public enum RelationshipType
{
    Ally,
    Neutral,
    Enemy
}
