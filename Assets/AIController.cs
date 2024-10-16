using UnityEngine;
using System.Collections.Generic;
using System.Collections;

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

    void Start()
    {
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
    }

    void InvestigateBehavior()
    {
        MoveTowards(targetPosition);
        // Check if arrived at the clue location
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Process the clue
            //FindObjectOfType<InvestigationManager>().ProcessClue(characterInstance.currentClue);
            // Return to Idle
            currentState = State.Idle;
        }
    }

    void SuspectBehavior()
    {
        CharacterInstance suspectedCharacter = FindSuspectedCharacter();
        if (suspectedCharacter != null)
        {
            targetPosition = suspectedCharacter.position;
            MoveTowards(targetPosition);
            // Check proximity to execute action
            if (Vector2.Distance(transform.position, targetPosition) < 1f)
            {
                ExecuteAction(suspectedCharacter);
                currentState = State.Idle;
            }
        }
        else
        {
            currentState = State.Idle;
        }
    }

    void ActingBehavior()
    {
        // Execute actions such as confronting or attacking
        // Implement based on game requirements
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
        FindObjectOfType<InvestigationManager>().KillCharacter(suspect.baseData.characterName);
    }
}