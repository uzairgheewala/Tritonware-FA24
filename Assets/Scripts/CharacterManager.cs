using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine.Events;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance { get; private set; }

    public List<CharacterBase> allCharacters;
    private Dictionary<string, CharacterInstance> activeCharacters;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            Logger.LogWarning("Duplicate CharacterManager instance destroyed.");
        }
        else
        {
            Instance = this;
            Logger.Log("CharacterManager instance created.");
        }
    }

    void Start()
    {
        InitializeCharacters();
        foreach (var character in activeCharacters.Values)
        {
            character.OnRelationshipChanged.AddListener(HandleRelationshipChange);
        }
    }

    void HandleRelationshipChange(string otherCharacterName, float newValue)
    {
        Logger.Log($"Relationship with {otherCharacterName} changed to {newValue}");
    }

    void InitializeCharacters()
    {
        activeCharacters = new Dictionary<string, CharacterInstance>();
        foreach (var character in allCharacters)
        {
            CharacterInstance instance = new CharacterInstance(character);
            activeCharacters.Add(character.characterName, instance);
            Logger.Log($"Character {character.characterName} initialized.");
        }
    }

    public CharacterInstance GetCharacter(string name)
    {
        if (activeCharacters.TryGetValue(name, out CharacterInstance instance))
            return instance;
        else
        {
            Logger.LogError($"Character not found: {name}");
            return null;
        }
    }

    public void UpdateRelationship(string characterA, string characterB, int delta)
    {
        var charA = GetCharacter(characterA);
        var charB = GetCharacter(characterB);

        if (charA != null)
            charA.UpdateRelationship(characterB, delta);

        if (charB != null)
            charB.UpdateRelationship(characterA, delta);
    }

    public List<CharacterInstance> GetAllCharacters()
    {
        return new List<CharacterInstance>(activeCharacters.Values);
    }
}

public class CharacterInstance
{
    public CharacterBase baseData;
    public Vector2 position;
    public List<ItemBase> currentInventory;
    public Dictionary<string, Relationship> currentRelationships;

    public UnityEvent<string, float> OnRelationshipChanged;
    public RuntimeAnimatorController animatorController;

    public CharacterInstance(CharacterBase baseCharacter)
    {
        baseData = baseCharacter;
        currentInventory = new List<ItemBase>(baseCharacter.inventory);
        currentRelationships = new Dictionary<string, Relationship>();
        OnRelationshipChanged = new UnityEvent<string, float>();

        foreach (var rel in baseCharacter.relationships)
        {
            currentRelationships.Add(rel.characterName, new Relationship
            {
                characterName = rel.characterName,
                relationshipValue = rel.relationshipValue
            });
        }

        Logger.Log($"CharacterInstance created for {baseData.characterName}");
    }

    public void UpdateRelationship(string characterName, int delta)
    {
        if (currentRelationships.TryGetValue(characterName, out Relationship relationship))
        {
            float oldValue = relationship.relationshipValue;
            relationship.relationshipValue = Mathf.Clamp(relationship.relationshipValue + delta, -100, 100);
            Logger.Log($"Relationship between {baseData.characterName} and {characterName} changed from {oldValue} to {relationship.relationshipValue}");
            OnRelationshipChanged.Invoke(characterName, relationship.relationshipValue);
        }
        else
        {
            Logger.LogWarning($"Relationship with {characterName} does not exist for {baseData.characterName}");
        }
    }

    public void DecayRelationships()
    {
        foreach (var relationship in currentRelationships.Values)
        {
            float oldValue = relationship.relationshipValue;
            if (relationship.relationshipValue > 0)
                relationship.relationshipValue = Mathf.Max(relationship.relationshipValue - baseData.decayRate * Time.deltaTime, 0);
            else if (relationship.relationshipValue < 0)
                relationship.relationshipValue = Mathf.Min(relationship.relationshipValue + baseData.decayRate * Time.deltaTime, 0);

            if (oldValue != relationship.relationshipValue)
            {
                Logger.Log($"Relationship with {relationship.characterName} decayed from {oldValue} to {relationship.relationshipValue}");
            }
        }
    }

    public void AddItem(ItemBase item)
    {
        currentInventory.Add(item);
        Logger.Log($"{baseData.characterName} added {item.itemName} to inventory.");
    }

    public void RemoveItem(ItemBase item)
    {
        currentInventory.Remove(item);
        Logger.Log($"{baseData.characterName} removed {item.itemName} from inventory.");
    }

    public bool HasItem(ItemBase item)
    {
        bool hasItem = currentInventory.Contains(item);
        Logger.Log($"{baseData.characterName} has item {item.itemName}: {hasItem}");
        return hasItem;
    }

    public Dialogue GenerateDialogue()
    {
        Logger.Log($"{baseData.characterName} is generating dialogue.");
        Dialogue dialogue = new Dialogue();
        return dialogue;
    }
}
