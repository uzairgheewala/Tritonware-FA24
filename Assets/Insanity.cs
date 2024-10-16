using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InsanityManager : MonoBehaviour
{
    public float insanityLevel = 0f;
    public float insanityThreshold = 100f;
    //public UIManager uiManager;
    public PlayerController playerController;

    void Update()
    {
        if (insanityLevel >= insanityThreshold)
        {
            TriggerInsanityEffects();
        }
    }

    public void IncreaseInsanity(float amount)
    {
        insanityLevel += amount;
        insanityLevel = Mathf.Clamp(insanityLevel, 0f, insanityThreshold);
        //uiManager.UpdateInsanityUI(insanityLevel);
    }

    public void DecreaseInsanity(float amount)
    {
        insanityLevel -= amount;
        insanityLevel = Mathf.Clamp(insanityLevel, 0f, insanityThreshold);
        //uiManager.UpdateInsanityUI(insanityLevel);
    }

    void TriggerInsanityEffects()
    {
        playerController.ApplyInsanityVisuals();

        // Apply audio distortions
        //AudioManager.Instance.ApplyInsanityAudioEffects();

        Debug.Log("Player has gone insane!");
    }
}