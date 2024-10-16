using UnityEngine;
using System.Collections.Generic;

public class InvestigationManager : MonoBehaviour
{
    public CharacterManager characterManager;
    public ClueManager clueManager;
    public InsanityManager insanityManager;

    private List<ClueBase> collectedClues = new List<ClueBase>();
    private float suspicionLevel = 0f;

    public void InvestigateCharacter(string characterName)
    {
        CharacterInstance character = characterManager.GetCharacter(characterName);
        if (character != null)
        {
            DisplayCharacterInfo(character);

            foreach (var motive in character.baseData.motives)
            {
                suspicionLevel += motive.intensity * 0.1f;
            }

            CheckSuspicionLevel();
        }
    }

    public void InvestigateScene(string sceneName)
    {
        List<ClueBase> sceneClues = clueManager.GetCluesInScene(sceneName);
        foreach (var clue in sceneClues)
        {
            CollectClue(clue);
        }
    }

    public void CollectClue(ClueBase clue)
    {
        if (!collectedClues.Contains(clue))
        {
            collectedClues.Add(clue);
            clueManager.DiscoverClue(clue.name);
            Debug.Log("Collected clue: " + clue.clueName);
            ProcessClue(clue);
        }
    }

    public void ProcessClue(ClueBase clue)
    {
        if (clue.associatedCharacter != "")
        {
            suspicionLevel += clue.suspicionIncrease;
            CheckSuspicionLevel();
        }
    }

    void DisplayCharacterInfo(CharacterInstance character)
    {
        // TODO: UI display logic
    }

    void CheckSuspicionLevel()
    {
        if (suspicionLevel > 80f)
        {
            insanityManager.IncreaseInsanity(10f);
            // Potentially kill a character based on high suspicion
            HandleHighSuspicion();
        }
    }

    void HandleHighSuspicion()
    {
        CharacterBase killer = DetermineKiller();
        if (killer != null)
        {
            characterManager.GetCharacter(killer.characterName).baseData.isKiller = true;
            KillCharacter(killer.characterName);
        }
    }

    CharacterBase DetermineKiller()
    {
        List<CharacterInstance> potentialKillers = characterManager.GetAllCharacters().FindAll(c => c.baseData.isKiller);
        if (potentialKillers.Count > 0)
            return potentialKillers[Random.Range(0, potentialKillers.Count)].baseData;
        return null;
    }

    public void KillCharacter(string characterName)
    {
        Debug.Log($"{characterName} has been killed due to high suspicion!");
        // Remove character from active characters
        // Implement visual and audio effects for death
    }

}