using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

[System.Serializable]
public class ItemData
{
    public string skinName;
    public Sprite skinSprite;
    public double skinReward;
    public Color rarityColor;
}

public class CrateManager : MonoBehaviour
{
    [Header("Settings")]
    public GameObject GameManagerObj;
    public TMP_Text moneyTMPText;
    public GameObject itemPrefab;
    public RectTransform content;
    public int itemsToSpawn = 50;
    public float itemWidth = 32f;
    public float smoothness = 2f;

    [Header("Possibilities")]
    public List<ItemData> possibleSkins;

    private float targetX;
    private float currentVelocity = 0f;
    private bool isSpinning = false;

    private ItemData winningItem;
    private int winnerIndex;

    public void StartSpinning()
    {
        GameManagerObj = GameObject.Find("GameManager");
        GameObject moneyTMP = GameObject.FindGameObjectWithTag("Money");
        moneyTMPText = moneyTMP.GetComponent<TMP_Text>();

        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        content.anchoredPosition = new Vector2(-100, 0);
        winnerIndex = itemsToSpawn - 10;

        winningItem = possibleSkins[Random.Range(0, possibleSkins.Count)];

        for (int i = 0; i < itemsToSpawn; i++)
        {
            ItemData skinToAssign;

            if (i == winnerIndex)
            {
                skinToAssign = winningItem;
            }
            else
            {
                skinToAssign = possibleSkins[Random.Range(0, possibleSkins.Count)];
            }

            GameObject newItem = Instantiate(itemPrefab, content);
            CaseItem itemScript = newItem.GetComponent<CaseItem>();
            if (itemScript != null)
            {
                itemScript.Setup(skinToAssign.skinName, skinToAssign.skinSprite, skinToAssign.skinReward);

            }

        }
        float randomOffset = Random.Range(-itemWidth * 0.5f, itemWidth * 0.1f);
        targetX = -(winnerIndex * itemWidth) + randomOffset + 160f;
        isSpinning = true;
    }

    public void CrateReward()
    {
        GameManager GameManager = GameManagerObj.GetComponent<GameManager>();
        moneyTMPText.SetText((GameManager.money += winningItem.skinReward).ToString());
    }

    private void Start()
    {
    }

    void Update()
    {
        if (isSpinning)
        {
            float newX = Mathf.SmoothDamp(content.anchoredPosition.x, targetX, ref currentVelocity, smoothness);
            content.anchoredPosition = new Vector2(newX, 0);

            if (Mathf.Abs(newX - targetX) <= 0.5f)
            {
                content.anchoredPosition = new Vector2(targetX, 0);
                isSpinning = false;
                Debug.Log("You unboxed: " + winningItem.skinName);
                Transform winningTransform = content.GetChild(winnerIndex);
                winningTransform.name = "WINNER";
                Debug.Log("CODE WINNER INDEX: " + winnerIndex);
                CrateReward();
            }
        }
    }
}
