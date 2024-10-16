using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public CharacterInstance characterInstance;

    public Image sanityOverlay; // Assign via Inspector
    public Camera mainCamera; // Assign via Inspector

    void Start()
    {
        Logger.Log("PlayerController initialized.");

        if (sanityOverlay == null)
        {
            Logger.LogWarning("Sanity overlay Image is not assigned in the Inspector.");
        }

        if (mainCamera == null)
        {
            Logger.LogWarning("Main Camera is not assigned in the Inspector.");
        }

        if (characterInstance == null)
        {
            Logger.LogWarning("CharacterInstance is not assigned.");
        }
    }

    public void ApplyInsanityVisuals()
    {
        Logger.Log("Applying insanity visuals.");
        StartCoroutine(InsanityVisualEffects());
    }

    private IEnumerator InsanityVisualEffects()
    {
        Logger.Log("Started insanity visual effects coroutine.");

        if (sanityOverlay != null)
        {
            sanityOverlay.color = new Color(1f, 0f, 0f, 0.2f);
            Logger.Log("Sanity overlay color changed to indicate insanity.");
        }
        else
        {
            Logger.LogWarning("Sanity overlay is null; cannot change color.");
        }

        StartCoroutine(ScreenShake());
        yield return new WaitForSeconds(5f); 

        if (sanityOverlay != null)
        {
            sanityOverlay.color = new Color(1f, 1f, 1f, 0f);
            Logger.Log("Sanity overlay color reset to normal.");
        }
    }

    private IEnumerator ScreenShake()
    {
        Logger.Log("Started screen shake coroutine.");

        if (mainCamera != null)
        {
            Vector3 originalPosition = mainCamera.transform.localPosition;

            for (int i = 0; i < 30; i++)
            {
                float x = Random.Range(-0.05f, 0.05f);
                float y = Random.Range(-0.05f, 0.05f);
                mainCamera.transform.localPosition = new Vector3(x, y, originalPosition.z);

                Logger.Log($"Screen shake iteration {i + 1}: Camera moved to ({x}, {y}, {originalPosition.z}).");

                yield return new WaitForSeconds(0.02f);
            }

            mainCamera.transform.localPosition = originalPosition;
            Logger.Log("Screen shake effect ended. Camera position reset.");
        }
        else
        {
            Logger.LogWarning("Main Camera is null; cannot perform screen shake.");
        }
    }
}