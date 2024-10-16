using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.Animations;

public class CharacterDisplay : MonoBehaviour, IPointerClickHandler
{
    public Image portraitImage; // Assign via Inspector
    public Text nameText;       // Assign via Inspector

    private CharacterInstance characterInstance;

    private Animator animator;
    public DialogueManager dialogueManager;

    public void Initialize(CharacterInstance instance)
    {
        characterInstance = instance;

        if (portraitImage != null)
            portraitImage.sprite = characterInstance.baseData.characterSprite;

        if (nameText != null)
            nameText.text = characterInstance.baseData.characterName;

        animator = GetComponent<Animator>();
        if (animator == null)
            animator = gameObject.AddComponent<Animator>();

        if (characterInstance.animatorController != null)
            animator.runtimeAnimatorController = characterInstance.animatorController;
        
        Logger.Log($"CharacterDisplay initialized for {characterInstance.baseData.characterName}");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Logger.Log($"Clicked on {characterInstance.baseData.characterName}");
        OnInteract();
    }

    public void OnInteract()
    {
        Dialogue dialogue = characterInstance.GenerateDialogue();
        dialogueManager.LoadDialogue(dialogue);
        dialogueManager.StartDialogue();
        Logger.Log($"{characterInstance.baseData.characterName} interaction started.");
    }

    public void SetWalking(bool isWalking)
    {
        animator.SetBool("IsWalking", isWalking);
        Logger.Log($"{characterInstance.baseData.characterName} walking state: {isWalking}");
    }

    public void SetInteracting(bool isInteracting)
    {
        animator.SetBool("IsInteracting", isInteracting);
        Logger.Log($"{characterInstance.baseData.characterName} interacting state: {isInteracting}");
    }

    public void SetSuspecting(bool isSuspecting)
    {
        animator.SetBool("IsSuspecting", isSuspecting);
        Logger.Log($"{characterInstance.baseData.characterName} suspecting state: {isSuspecting}");
    }

    public void Die()
    {
        animator.SetTrigger("Die");
        Logger.Log($"{characterInstance.baseData.characterName} has died.");
    }

    public void UpdateDirection(Vector2 direction)
    {
        Logger.Log($"{characterInstance.baseData.characterName} direction updated to {direction}");
        // Implement direction update logic if needed
    }
}