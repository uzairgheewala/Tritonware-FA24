using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Game Data/Item")]
public class ItemBase : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite itemSprite;
    public ItemType itemType;
}

public enum ItemType
{
    Clue,
    Weapon,
    Tool,
    Consumable,
    Key
}