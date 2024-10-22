using UnityEngine;
using UnityEngine.UI;

public class ItemDetailsPanel : MonoBehaviour
{
    public static ItemDetailsPanel Instance { get; private set; }
    public Text itemNameText; // Assign in Inspector
    public Text itemDescriptionText; // Assign in Inspector
    public Image itemImage; // Assign in Inspector

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            gameObject.SetActive(false); // Initially hide the panel
        }
    }

    public void ShowItemDetails(ItemBase item)
    {
        itemNameText.text = item.itemName;
        itemDescriptionText.text = item.description;
        itemImage.sprite = item.itemSprite;

        gameObject.SetActive(true); // Show the panel
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false); // Hide the panel
    }
}
