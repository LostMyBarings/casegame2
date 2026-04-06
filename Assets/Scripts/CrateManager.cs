using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

[System.Serializable]
public class ItemData
{
    public string skinName;
    public Sprite skinSprite;
    public Color rarityColor;
}

public class CrateManager : MonoBehaviour
{
    [Header("Settings")]
    public GameObject itemPrefab;
    public RectTransform content;
    public int itemsToSpawn = 100;
    public float itemWidth = 32f;
    public float smoothness = 5f;

    [Header("Possibilities")]
    public List<ItemData> possibleSkins;

    private float targetX;
    private float currentVelocity = 0f;
    private bool isSpinning = false;

    public void StartSpinning()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        content.anchoredPosition = new Vector2(-100, 0);

        for (int i = 0; i < itemsToSpawn; i++)
        {
            ItemData randomSkin = possibleSkins[Random.Range(0, possibleSkins.Count)];
            GameObject newItem = Instantiate(itemPrefab, content);
        }

        int winnerIndex = itemsToSpawn - 20;
        float randomOffset = Random.Range(-itemWidth * 0.45f, itemWidth * 0.45f);
        targetX = -(winnerIndex * itemWidth) + randomOffset;
        isSpinning = true;
    }

    void Start()
    {
        
    }
    void Update()
    {
        if (isSpinning)
        {
            Debug.Log("Spinning Started");
            float newX = Mathf.SmoothDamp(content.anchoredPosition.x, targetX, ref currentVelocity, smoothness);
            content.anchoredPosition = new Vector2(newX, 0);

            if (Mathf.Abs(newX - targetX) <= 0.4f)
            {
                content.anchoredPosition = new Vector2(targetX, 0);
                isSpinning = false;
            }
        }
        if (!isSpinning)
        {
            Debug.Log("Spinning ended");
        }
    }
}
