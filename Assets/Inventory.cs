using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
<<<<<<< Updated upstream:Assets/Inventory.cs
    public GameObject inventoryPanel; // Assign in Inspector
    public Transform itemsParent; 
    public InventorySlot[] slots;
=======
    public GameObject inventorySlotPrefab; // Prefab for the inventory slot
    public Transform itemsParent; // Parent object for inventory slots
    public GameObject itemDescriptionPanel; // Reference to the item description panel UI
    public TMP_Text itemDescriptionText; // Reference to the text component in the panel

    private List<InventorySlot> slots = new List<InventorySlot>();
>>>>>>> Stashed changes:Assets/Scripts/InventoryManager.cs

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

    private void Start()
    {
<<<<<<< Updated upstream:Assets/Inventory.cs
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
=======
        // Get all InventorySlot components in the children of itemsParent
        slots.AddRange(itemsParent.GetComponentsInChildren<InventorySlot>());
>>>>>>> Stashed changes:Assets/Scripts/InventoryManager.cs
    }

    public void UpdateInventory(CharacterInstance character)
    {
<<<<<<< Updated upstream:Assets/Inventory.cs
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < character.currentInventory.Count)
            {
                slots[i].AddItem(character.currentInventory[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
=======
        // Clear all slots first
        foreach (var slot in slots)
        {
            slot.ClearSlot(); // Clear existing items in the slots
        }

        // Populate the inventory slots based on the character's current inventory
        for (int i = 0; i < character.currentInventory.Count && i < slots.Count; i++)
        {
            slots[i].AddItem(character.currentInventory[i]); // Add items to the first empty slots
>>>>>>> Stashed changes:Assets/Scripts/InventoryManager.cs
        }
    }

    public void ShowItemDescription(string description)
    {
        itemDescriptionText.text = description;
        itemDescriptionPanel.SetActive(true); // Show the description panel
    }

    public void HideItemDescription()
    {
        itemDescriptionPanel.SetActive(false); // Hide the description panel
    }
}
<<<<<<< Updated upstream:Assets/Inventory.cs

public class InventorySlot : MonoBehaviour
{
    public Image icon; // Assign in Inspector
    public Button removeButton; // Assign in Inspector
    private ItemBase item;

    public void AddItem(ItemBase newItem)
    {
        item = newItem;
        icon.sprite = item.itemSprite;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    /*
    public void OnRemoveButton()
    {
        CharacterInstance playerCharacter = 
        playerCharacter.RemoveItem(item);
        InventoryManager.Instance.UpdateInventoryUI(playerCharacter);
    }

    public void UseItem()
    {
        if (item != null)
        {
            CharacterInstance playerCharacter = 
            ItemManager.Instance.UseItem(item, playerCharacter);
            InventoryManager.Instance.UpdateInventoryUI(playerCharacter);
        }
    }*/
}
=======
>>>>>>> Stashed changes:Assets/Scripts/InventoryManager.cs
