using UnityEngine;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{
    public List<CharacterBase> allCharacters;
    private Dictionary<string, CharacterInstance> activeCharacters;

    void Start()
    {
        InitializeCharacters();
    }

    void InitializeCharacters()
    {
        activeCharacters = new Dictionary<string, CharacterInstance>();
        foreach (var character in allCharacters)
        {
            CharacterInstance instance = new CharacterInstance(character);
            activeCharacters.Add(character.characterName, instance);
        }
    }

}

public class CharacterInstance
{
    public CharacterBase baseData;
    public List<ItemBase> currentInventory;
    public Dictionary<string, Relationship> currentRelationships;

    public CharacterInstance(CharacterBase baseCharacter)
    {
        baseData = baseCharacter;
        currentInventory = new List<ItemBase>(baseCharacter.inventory);

        currentRelationships = new Dictionary<string, Relationship>();
        foreach (var rel in baseCharacter.relationships.relationshipLevels)
        {
            currentRelationships.Add(rel.Key, baseCharacter.relationships);
        }
    }

}