using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public CharacterInstance characterInstance;

    public Image sanityOverlay; // Assign via Inspector
    public Camera mainCamera; // Assign via Inspector

    public void ApplyInsanityVisuals()
    {
        StartCoroutine(InsanityVisualEffects());
    }

    private IEnumerator InsanityVisualEffects()
    {
        sanityOverlay.color = new Color(1f, 0f, 0f, 0.2f);
        StartCoroutine(ScreenShake());
        yield return new WaitForSeconds(5f); 
        sanityOverlay.color = new Color(1f, 1f, 1f, 0f);
    }

    private IEnumerator ScreenShake()
    {
        Vector3 originalPosition = mainCamera.transform.localPosition;
        for (int i = 0; i < 30; i++)
        {
            float x = Random.Range(-0.05f, 0.05f);
            float y = Random.Range(-0.05f, 0.05f);
            mainCamera.transform.localPosition = new Vector3(x, y, originalPosition.z);
            yield return new WaitForSeconds(0.02f);
        }
        mainCamera.transform.localPosition = originalPosition;
    }
}