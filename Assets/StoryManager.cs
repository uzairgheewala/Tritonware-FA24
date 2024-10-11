using UnityEngine;
using System.Collections.Generic;

public class StoryManager : MonoBehaviour
{
    public CharacterManager characterManager;
    public List<CharacterBase> possibleKillers;
    private CharacterBase selectedKiller;

    void Start()
    {
        SelectKiller();
        GenerateStoryline();
    }

    void SelectKiller()
    {
        selectedKiller = possibleKillers[Random.Range(0, possibleKillers.Count)];
        selectedKiller.isKiller = true;
    }

    public void GenerateStoryline()
    {
        foreach (var character in characterManager.GetAllCharacters())
        {
            if (character.baseData != selectedKiller)
            {
                // Assign motives pointing towards the killer
                foreach (var motive in character.baseData.motives)
                {
                    if (motive.motiveDescription.Contains("resentment") || motive.motiveDescription.Contains("jealousy"))
                    {
                        motive.intensity += 10; // Increase motive intensity towards the killer
                    }
                }

                // Adjust relationships to decrease trust towards the killer
                /*
                foreach (var relatedCharacter in character.currentRelationships.relationshipLevels.Keys)
                {
                    if (relatedCharacter == selectedKiller.characterName)
                    {
                        character.currentRelationships.relationshipLevels[relatedCharacter] -= 20;
                    }
                }*/
            }
        }

        // Optionally, adjust scene triggers or events based on the killer
    }

    public CharacterBase GetSelectedKiller()
    {
        return selectedKiller;
    }

}