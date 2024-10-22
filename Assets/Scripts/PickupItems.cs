using UnityEngine;

public class PickupItems : MonoBehaviour
{
    public ItemBase itemData; // Reference to the ItemBase ScriptableObject
    private SpriteRenderer spriteRenderer;

    public void Initialize(ItemBase data)
    {
        itemData = data;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemData.itemSprite; // Set the sprite for the item
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Item picked up by player!");
            CharacterInstance character = collision.GetComponent<PlayerController>().characterInstance;
            character.AddItem(itemData); // Add item to the player's inventory
            InventoryManager.Instance.UpdateInventory(character); // Update the inventory UI
            Destroy(gameObject); // Destroy the item pickup after it's collected
        }
    }
}
