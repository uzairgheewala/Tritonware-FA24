using UnityEngine;
using System.Collections.Generic;


public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; private set; }

    public List<InteractionBase> availableInteractions;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowInteractions(CharacterInstance initiator, CharacterInstance target)
    {
        // Display interaction options in the UI
        // For example, populate a menu with availableInteractions
    }

    public void PerformInteraction(CharacterInstance initiator, CharacterInstance target, InteractionBase interaction)
    {
        //initiator.InteractWith(target, interaction);
    }
}