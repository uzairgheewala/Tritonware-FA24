using UnityEngine;
using System.Collections.Generic;

public class ClueManager : MonoBehaviour
{
    public List<ClueBase> allClues; // Assign via Inspector
    private Dictionary<string, bool> discoveredClues;

    void Start()
    {
        InitializeClues();
    }

    void InitializeClues()
    {
        discoveredClues = new Dictionary<string, bool>();
        foreach (var clue in allClues)
        {
            discoveredClues.Add(clue.clueName, false);
        }
    }

    public void AssignCluesToKiller(CharacterBase killer)
    {
        foreach (var clue in allClues)
        {
            if (clue.associatedCharacter == killer.characterName)
            {
                clue.isAssignedToKiller = true;
            }
        }
    }

    public void DiscoverClue(string clueName)
    {
        if (discoveredClues.ContainsKey(clueName))
        {
            discoveredClues[clueName] = true;
            ClueBase clue = allClues.Find(c => c.clueName == clueName);
            if (clue != null)
            {
                Debug.Log("Discovered Clue: " + clue.clueName);
            }
        }
    }

    public bool IsClueDiscovered(string clueName)
    {
        return discoveredClues.ContainsKey(clueName) && discoveredClues[clueName];
    }

    public List<ClueBase> GetCluesInScene(string sceneName)
    {
        return allClues.FindAll(c => c.sceneName == sceneName && !c.isDiscovered);
    }
}

