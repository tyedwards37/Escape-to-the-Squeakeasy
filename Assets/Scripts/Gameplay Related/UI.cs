using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [Header("Hint")]
    [SerializeField] Transform hint;

    [Header("Inventory System")]
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject keyDisplayPrefab;
    [SerializeField] InventoryItems inventoryItems;
    Dictionary<string, Sprite> inventoryItemsDictionary;

    [Header("Pause System")]
    [SerializeField] Image pauseImage;
    [SerializeField] Sprite pauseSprite;
    [SerializeField] Sprite playSprite;
    [SerializeField] GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        inventoryItemsDictionary = inventoryItems.ToDictionary();
        ResetUI();
    }

    public void TriggerHintExit()
    {
        if(hint.gameObject.activeInHierarchy)
        {
            Vector3 targetPos = hint.position;
            hint.LeanMoveX(targetPos.x - 1000f, 3f).setEase(LeanTweenType.animationCurve).setOnComplete(() =>
            {
                hint.gameObject.SetActive(false);
            });
        }
    }

    public void ShowInventoryItem(string name)
    {
        GameObject key = Instantiate(keyDisplayPrefab, inventoryPanel.transform);
        key.GetComponent<Image>().sprite = inventoryItemsDictionary[name];

        if(!inventoryPanel.activeInHierarchy) {
            FlyInventoryPanelInFromOffscreen();
        }
    }

    public void PressedPauseButton()
    {
        Game.Instance.TogglePause();
    }

    public void TogglePausedUI()
    {
        pauseMenu.SetActive(Game.Instance.isGamePaused);
        pauseImage.sprite = Game.Instance.isGamePaused ? playSprite : pauseSprite;
    }

    private void FlyInventoryPanelInFromOffscreen()
    {
        Vector3 targetPos = inventoryPanel.transform.position;
        inventoryPanel.transform.LeanMoveX(targetPos.x - 300f, 0f);
        inventoryPanel.SetActive(true);

        // Animate the inventory panel from offscreen to its current position
        inventoryPanel.transform.LeanMoveX(targetPos.x, 0.5f).setEase(LeanTweenType.animationCurve);
    }

    private void ResetUI()
    {
        inventoryPanel.SetActive(false);
        pauseMenu.SetActive(false);
        hint.gameObject.SetActive(true);
    }
}

[Serializable]
public class InventoryItems
{
    [SerializeField] private InventoryItem[] inventoryItems;

    public Dictionary<string, Sprite> ToDictionary()
    {
        Dictionary<string, Sprite> newDict = new Dictionary<string, Sprite>();

        foreach(var item in inventoryItems) {
            newDict.Add(item.itemName, item.itemVisual);
        }

        return newDict;
    }
}

[Serializable]
public class InventoryItem
{
    [SerializeField] public string itemName;
    [SerializeField] public Sprite itemVisual;
}