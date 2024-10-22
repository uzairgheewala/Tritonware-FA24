using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class ItemBase : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite itemSprite;
    public ItemType itemType;
    public Vector2 position;
}

public enum ItemType
{
    Clue,
    Weapon,
    Tool,
    Consumable,
    Key
}
