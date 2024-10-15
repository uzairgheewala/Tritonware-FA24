using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public GameObject inventoryPanel; // Assign in Inspector
    public Transform itemsParent; 
    public InventorySlot[] slots;

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
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    public void UpdateInventory(CharacterInstance character)
    {
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
        }
    }
}

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