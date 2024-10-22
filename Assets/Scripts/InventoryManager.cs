using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public GameObject inventorySlotPrefab; // New field for the slot prefab
    public Transform itemsParent; // The parent object for inventory slots

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
        //slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        slots.AddRange(itemsParent.GetComponentsInChildren<InventorySlot>());
    }

    public void UpdateInventory(CharacterInstance character)
    {
        foreach (var slot in slots)
        {
            Destroy(slot.gameObject);
        }
        slots.Clear();

        // Create new slots based on character's inventory
        foreach (var item in character.currentInventory)
        {
            GameObject slotGO = Instantiate(inventorySlotPrefab, itemsParent);
            InventorySlot slot = slotGO.GetComponent<InventorySlot>();
            slot.AddItem(item);
            slots.Add(slot);
        }
    }
}

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
    }*/
}