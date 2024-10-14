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
        } 
        else 
        { 
            Instance = this; 
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
        Debug.Log($"Relationship with {otherCharacterName} changed to {newValue}");
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

    public CharacterInstance GetCharacter(string name)
    {
        if (activeCharacters.TryGetValue(name, out CharacterInstance instance))
            return instance;
        else
        {
            Debug.LogError($"Character not found: {name}");
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
    }

    public void UpdateRelationship(string characterName, int delta)
    {
        if (currentRelationships.TryGetValue(characterName, out Relationship relationship))
        {
            relationship.relationshipValue = Mathf.Clamp(relationship.relationshipValue + delta, -100, 100);
            OnRelationshipChanged.Invoke(characterName, relationship.relationshipValue);
        }
    }

    public void DecayRelationships()
    {
        foreach (var relationship in currentRelationships.Values)
        {
            if (relationship.relationshipValue > 0)
                relationship.relationshipValue = Mathf.Max(relationship.relationshipValue - baseData.decayRate * Time.deltaTime, 0);
            else if (relationship.relationshipValue < 0)
                relationship.relationshipValue = Mathf.Min(relationship.relationshipValue + baseData.decayRate * Time.deltaTime, 0);
        }
    }

    public void AddItem(ItemBase item)
    {
        currentInventory.Add(item);
    }

    public void RemoveItem(ItemBase item)
    {
        currentInventory.Remove(item);
    }

    public bool HasItem(ItemBase item)
    {
        return currentInventory.Contains(item);
    }

    public Dialogue GenerateDialogue()
    {
        Dialogue d = new Dialogue();//"Uzi", new List(new Sentence("Hello", new List(new Choice("h", "h")))));
        return d;
    }
}
