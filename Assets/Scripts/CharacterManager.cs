using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance { get; private set; }

    public List<CharacterBase> allCharacters;
    public GameObject npcPrefab; // Assign the NPC prefab here
    public string targetSceneName = "Living Room"; // Ensure this matches your scene name
    public string spawnPointName = "spawnPoint_LR"; // Name of the spawn point GameObject in LivingRoom

    public Transform livingRoomSpawnPoint; // Assign a spawn point in LivingRoom scene
    private Dictionary<string, CharacterInstance> activeCharacters;
    private Dictionary<string, GameObject> characterGameObjects;

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

        // Start the spawning process
        StartCoroutine(EnsureSceneAndSpawnCharacters());
    }

    void HandleRelationshipChange(string otherCharacterName, float newValue)
    {
        Logger.Log($"Relationship with {otherCharacterName} changed to {newValue}");
    }

    void InitializeCharacters()
    {
        activeCharacters = new Dictionary<string, CharacterInstance>();
        characterGameObjects = new Dictionary<string, GameObject>();

        foreach (var character in allCharacters)
        {
            CharacterInstance instance = new CharacterInstance(character);
            activeCharacters.Add(character.characterName, instance);
            Logger.Log($"Character {character.characterName} initialized.");

            // Instantiate NPC in the LivingRoom scene
            //SpawnCharacterInScene(character, "Living Room");
        }
    }

    IEnumerator EnsureSceneAndSpawnCharacters()
    {
        // Check if LivingRoom is already loaded
        Scene livingRoomScene = SceneManager.GetSceneByName(targetSceneName);
        if (!livingRoomScene.IsValid())
        {
            // Load LivingRoom additively
            Logger.Log($"Loading scene: {targetSceneName}");
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Additive);

            // Wait until the scene is loaded
            while (!loadOperation.isDone)
            {
                yield return null;
            }

            livingRoomScene = SceneManager.GetSceneByName(targetSceneName);
            if (!livingRoomScene.IsValid())
            {
                Logger.LogError($"Failed to load scene: {targetSceneName}");
                yield break;
            }
            Logger.Log($"Scene {targetSceneName} loaded successfully.");
        }
        else
        {
            Logger.Log($"Scene {targetSceneName} is already loaded.");
        }

        // Set LivingRoom as the active scene
        /*
        bool setActive = SceneManager.SetActiveScene(livingRoomScene);
        if (setActive)
        {
            Logger.Log($"Active scene set to: {livingRoomScene.name}");
        }
        else
        {
            Logger.LogWarning($"Failed to set active scene to: {livingRoomScene.name}");
            //yield break;
        }*/

        // Find the spawn point in LivingRoom
        GameObject spawnPointGO = GameObject.Find(spawnPointName);
        if (spawnPointGO == null)
        {
            Logger.LogWarning($"Spawn point '{spawnPointName}' not found in scene {livingRoomScene.name}.");
            //yield break;
        }

        Transform spawnPoint = spawnPointGO.transform;
        Logger.Log($"Spawn point '{spawnPointName}' found at position {spawnPoint.position}.");

        // Spawn all NPCs
        foreach (var character in allCharacters)
        {
            SpawnCharacterInScene(character, targetSceneName);
            yield return null; // Yield to allow other operations
        }
    }

    void SpawnCharacterInScene(CharacterBase character, string sceneName)
    {
        // Ensure the spawn point is assigned
        if (livingRoomSpawnPoint == null)
        {
            Logger.LogError("livingRoomSpawnPoint is not assigned in CharacterManager.");
            return;
        }

        // Check if the current active scene is the target scene
        Logger.Log($"Current active scene: {SceneManager.GetActiveScene().name}");
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            Logger.Log($"Current scene is not {sceneName}. Skipping spawn for {character.characterName}.");
            return;
        }

        // Instantiate NPC prefab
        GameObject npcGO = Instantiate(npcPrefab, livingRoomSpawnPoint.position, Quaternion.identity);
        npcGO.name = character.characterName;

        // Initialize CharacterDisplay
        CharacterDisplay display = npcGO.GetComponent<CharacterDisplay>();
        if (display != null)
        {
            display.Initialize(activeCharacters[character.characterName]);
        }
        else
        {
            Logger.LogError($"CharacterDisplay component missing on NPC prefab for {character.characterName}.");
        }

        // Initialize CharacterAI
        CharacterAI ai = npcGO.GetComponent<CharacterAI>();
        if (ai != null)
        {
            ai.characterInstance = activeCharacters[character.characterName];
            Logger.Log($"CharacterAI initialized for {character.characterName}.");
        }
        else
        {
            Logger.LogError($"CharacterAI component missing on NPC prefab for {character.characterName}.");
        }

        // Add to game objects dictionary
        characterGameObjects.Add(character.characterName, npcGO);

        Logger.Log($"NPC {character.characterName} spawned in {sceneName} at position {livingRoomSpawnPoint.position}.");
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
