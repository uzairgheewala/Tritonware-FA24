using UnityEngine;
using System.Collections.Generic;

public class ItemEvent : UnityEngine.Events.UnityEvent<ItemBase> { }

public class ItemManager : MonoBehaviour
{
    public ItemEvent OnItemPickedUp = new ItemEvent();
    public ItemEvent OnItemUsed = new ItemEvent();

    public static ItemManager Instance { get; private set; }

    public List<ItemBase> allItems;
    public GameObject itemPrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    public void SpawnItem(ItemBase itemData, Vector2 position)
    {
        GameObject itemGO = Instantiate(itemPrefab, position, Quaternion.identity);
        ItemPickup itemPickup = itemGO.GetComponent<ItemPickup>();
        itemPickup.Initialize(itemData);
    }

    public void PickUpItem(ItemBase item, CharacterInstance character)
    {
        character.AddItem(item);
        Debug.Log($"{character.baseData.characterName} picked up {item.itemName}");
        OnItemPickedUp.Invoke(item);
    }

    public void UseItem(ItemBase item, CharacterInstance character)
    {
        if (character.HasItem(item))
        {
            OnItemUsed.Invoke(item);

            if (item.itemType == ItemType.Consumable)
            {
                character.RemoveItem(item);
                Debug.Log($"{item.itemName} consumed by {character.baseData.characterName}");
            }
        }
        else
        {
            Debug.LogError($"{character.baseData.characterName} does not have {item.itemName} in inventory");
        }
    }

}

public class ItemPickup : MonoBehaviour
{
    public ItemBase itemData;
    private SpriteRenderer spriteRenderer;

    public void Initialize(ItemBase data)
    {
        itemData = data;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemData.itemSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CharacterInstance character = collision.GetComponent<PlayerController>().characterInstance;
            ItemManager.Instance.PickUpItem(itemData, character);
            Destroy(gameObject);
        }
    }
}

public interface IUsable
{
    void Use(CharacterInstance character);
}