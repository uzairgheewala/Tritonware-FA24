using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class CharacterAI : MonoBehaviour
{
    public enum State
    {
        Idle,
        Exploring,
        Suspecting,
        Acting
    }

    public State currentState = State.Idle;
    public CharacterInstance characterInstance;
    public float moveSpeed = 2f;
    private Vector2 targetPosition;
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(StateMachine());
    }

    IEnumerator StateMachine()
    {
        while (true)
        {
            switch (currentState)
            {
                case State.Idle:
                    IdleBehavior();
                    break;
                case State.Exploring:
                    InvestigateBehavior();
                    break;
                case State.Suspecting:
                    SuspectBehavior();
                    break;
                case State.Acting:
                    ActingBehavior();
                    break;
            }
            yield return null;
        }
    }

    void IdleBehavior()
    {
        // Move randomly within the manor
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = GetRandomPosition();
        }
        MoveTowards(targetPosition);
        SetWalking(true);
    }

    void InvestigateBehavior()
    {
        // Similar to IdleBehavior or customized
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = GetRandomPosition();
        }
        MoveTowards(targetPosition);
        SetWalking(true);
    }

    void SuspectBehavior()
    {
        CharacterInstance suspectedCharacter = FindSuspectedCharacter();
        if (suspectedCharacter != null)
        {
            targetPosition = suspectedCharacter.position;
            MoveTowards(targetPosition);
            SetWalking(true);

            // Check proximity to execute action
            if (Vector2.Distance(transform.position, targetPosition) < 1f)
            {
                ExecuteAction(suspectedCharacter);
                currentState = State.Idle;
                SetWalking(false);
            }
        }
        else
        {
            currentState = State.Idle;
            SetWalking(false);
        }
    }

    void ActingBehavior()
    {
        // Execute actions such as confronting or attacking
        // Implement based on game requirements
        SetWalking(false);
    }

    Vector2 GetRandomPosition()
    {
        float x = Random.Range(-10f, 10f);
        float y = Random.Range(-10f, 10f);
        return new Vector2(x, y);
    }

    void MoveTowards(Vector2 position)
    {
        Vector2 direction = (position - (Vector2)transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        UpdateAnimator(direction);
    }

    void UpdateAnimator(Vector2 direction)
    {
        if (animator != null)
        {
            animator.SetFloat("InputX", direction.x);
            animator.SetFloat("InputY", direction.y);
            animator.SetBool("isWalking", true);
        }
    }

    void SetWalking(bool isWalking)
    {
        if (animator != null)
        {
            animator.SetBool("isWalking", isWalking);
        }
    }

    CharacterInstance FindSuspectedCharacter()
    {
        List<CharacterInstance> allCharacters = FindObjectOfType<CharacterManager>().GetAllCharacters();
        CharacterInstance suspect = null;
        float highestSuspicion = 0;
        foreach (var character in allCharacters)
        {
            if (character.baseData.isKiller && character != characterInstance)
            {
                if (characterInstance.currentRelationships.ContainsKey(character.baseData.characterName))
                {
                    float suspicion = characterInstance.currentRelationships[character.baseData.characterName].relationshipValue;
                    if (suspicion > highestSuspicion)
                    {
                        highestSuspicion = suspicion;
                        suspect = character;
                    }
                }
            }
        }
        return suspect;
    }

    void ExecuteAction(CharacterInstance suspect)
    {
        Logger.Log($"{characterInstance.baseData.characterName} is executing action on {suspect.baseData.characterName}");
    }
}