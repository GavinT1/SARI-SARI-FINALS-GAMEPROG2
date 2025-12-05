using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomerReceiptItem : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public Image itemIconImage; // Use SpriteRenderer if you are not using UI components

    public void Setup(ItemData item)
    {
        if (itemNameText != null)
        {
            itemNameText.text = item.itemName;
        }
        
        // --- THIS SECTION IS NOW UNCOMMENTED AND COMPLETE ---
        if (itemIconImage != null)
        {
            if (item.itemIcon != null)
            {
                // Set the sprite from the ItemData and ensure the Image component is active
                itemIconImage.sprite = item.itemIcon;
                itemIconImage.enabled = true; 
            }
            else
            {
                // Hide the image component if no icon is provided
                itemIconImage.enabled = false;
            }
        }
    }
}