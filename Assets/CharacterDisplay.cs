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
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnInteract();
    }

    public void OnInteract()
    {
        Dialogue dialogue = characterInstance.GenerateDialogue();
        dialogueManager.StartDialogue(dialogue);
    }

    public void SetWalking(bool isWalking)
    {
        animator.SetBool("IsWalking", isWalking);
    }

    public void SetInteracting(bool isInteracting)
    {
        animator.SetBool("IsInteracting", isInteracting);
    }

    public void SetSuspecting(bool isSuspecting)
    {
        animator.SetBool("IsSuspecting", isSuspecting);
    }

    public void Die()
    {
        animator.SetTrigger("Die");
    }

    public void UpdateDirection(Vector2 direction)
    {
        
    }
}