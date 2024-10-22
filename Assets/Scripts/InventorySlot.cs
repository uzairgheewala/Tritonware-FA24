using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon; // Assign in Inspector
    public Text itemNameText; // Assign in Inspector
    private ItemBase item;

    public void AddItem(ItemBase newItem)
    {
        item = newItem;
        if (item != null)
        {
            icon.sprite = item.itemSprite; // Load the item's image
            icon.enabled = true; // Show the icon
            itemNameText.text = item.itemName; // Update the item's name
        }
        else
        {
            ClearSlot(); // Clear slot if item is null
        }
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null; // Clear the icon
        icon.enabled = false; // Hide the icon
        itemNameText.text = ""; // Clear the item name text
    }

    public ItemBase GetItem()
    {
        return item; // Return the current item
    }
}
