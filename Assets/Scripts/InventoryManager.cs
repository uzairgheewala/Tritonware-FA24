using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public GameObject inventorySlotPrefab; // Prefab for the inventory slot
    public Transform itemsParent; // Parent object for inventory slots
    public GameObject itemDescriptionPanel; // Reference to the item description panel UI
    public TMP_Text itemDescriptionText; // Reference to the text component in the panel

    private List<InventorySlot> slots = new List<InventorySlot>();

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
            DontDestroyOnLoad(this.gameObject);
        } 
    }

    private void Start()
    {
        // Get all InventorySlot components in the children of itemsParent
        slots.AddRange(itemsParent.GetComponentsInChildren<InventorySlot>());
    }

    public void UpdateInventory(CharacterInstance character)
    {
        // Clear all slots first
        foreach (var slot in slots)
        {
            slot.ClearSlot(); // Clear existing items in the slots
        }

        // Populate the inventory slots based on the character's current inventory
        for (int i = 0; i < character.currentInventory.Count && i < slots.Count; i++)
        {
            slots[i].AddItem(character.currentInventory[i]); // Add items to the first empty slots
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

/*
public class InventorySlot : MonoBehaviour
{
    public Image icon; // Assign in Inspector
    public Text itemNameText; // Assign in Inspector
    private ItemBase item;

    public void AddItem(ItemBase newItem)
    {
        item = newItem;
        icon.sprite = item.itemSprite;
        icon.enabled = true;
        itemNameText.text = item.itemName;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        itemNameText.text = "";
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
    }
}*/
