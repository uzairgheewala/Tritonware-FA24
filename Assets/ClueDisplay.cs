using UnityEngine;
using UnityEngine.UI;

public class ClueDisplay : MonoBehaviour
{
    public ClueBase clue;
    public Image clueImage; // Assign via Inspector
    public Text clueDescriptionText; // Assign via Inspector

    public void Initialize(ClueBase clueBase)
    {
        clue = clueBase;
        clueImage.sprite = clueBase.clueSprite; 
        clueDescriptionText.text = clueBase.description;
    }

    public void OnCollectClue()
    {
        FindObjectOfType<InvestigationManager>().CollectClue(clue);
        gameObject.SetActive(false); // Hide the clue after collection
    }
}