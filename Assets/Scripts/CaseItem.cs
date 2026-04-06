using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CaseItem : MonoBehaviour
{
    public Image itemImage;
    public TextMeshProUGUI itemName;

    public void Setup(string newName, Sprite newSprite)
    {
        if (itemImage != null) itemImage.sprite = newSprite;
        if (itemName != null) itemName.text = newName;
    }
}