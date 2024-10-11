using UnityEngine;

public class InteractionBase : ScriptableObject
{
    public string interactionName;
    public int relationshipEffect; 
    public Sprite icon; 
    public InteractionType interactionType;
    
    public virtual void Execute(CharacterInstance initiator, CharacterInstance target)
    {
        target.UpdateRelationship(initiator.baseData.characterName, relationshipEffect);
    }
}

public enum InteractionType
{
    Conversation,
    Gift,
    Help,
    Confrontation
}

public class InteractionEffect
{
    public string targetCharacterName;
    public int relationshipChange;
    public Trait newTrait;
    public Motive newMotive;
}