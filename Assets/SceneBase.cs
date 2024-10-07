using UnityEngine;
using System.Collections.Generic;

public class SceneBase : ScriptableObject
{
    public string sceneName;
    public List<SceneItem> sceneItems;
    public List<SceneCharacter> sceneCharacters;
    public List<SceneTrigger> sceneTriggers;
}

public class SceneItem
{
    public ItemBase item;
    public Vector2 position;
}

public class SceneCharacter
{
    public CharacterInstance character;
    public Vector2 position;
}

public class SceneTrigger
{
    public string triggerName;
    public Vector2 position;
    public TriggerType triggerType;
    public string targetScene;
}

public enum TriggerType
{
    Door,
    Event,
    Puzzle
}