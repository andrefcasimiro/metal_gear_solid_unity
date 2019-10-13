using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {

  [Header("UI")]
  public GameObject inventoryUI;
  public GameObject itemButtonPrefab;

  [HideInInspector]
  public Inventory inventory;

  private GameController gameController;
  private EquipmentManager equipmentManager;
  private bool isActive;

  private void Awake ()
  {
    inventory = new Inventory();

    // Don't show inventory ui on awake
    inventoryUI.SetActive(false);
  }

  private void Start ()
  {
    equipmentManager = GetComponent<EquipmentManager>();
    gameController = GameObject.FindWithTag(Constants.GAME_CONTROLLER).GetComponent<GameController>();
  }

  private void Update ()
  {
    if (Input.GetButtonDown(Constants.INVENTORY))
    {
      isActive = !isActive;
      inventoryUI.SetActive(isActive);

      if (isActive)
      {
        Draw();
        gameController.Pause();
      }
      else
      {
        gameController.Resume();
      }
    }
  }

  private void Draw ()
  {
    GameObject panel = inventoryUI.transform.GetChild(0).gameObject;

    // Clean panel first
    foreach (Transform child in panel.transform)
    {
      Destroy(child.gameObject);
    }

    // Store reference for performance
    List<ScriptableItem> inventoryItems = inventory.ListAll();

    List<ScriptableItem> visited = new List<ScriptableItem>();
    List<GameObject> visitedButtons = new List<GameObject>();

    foreach (ScriptableItem item in inventoryItems)
    {
      GameObject itemButton = null;

      int visitedIndex = item.stackable ? visited.FindIndex(entry => entry.name == item.name) : -1;

      // Check if visited contains this item
      if (visitedIndex != -1)
      {
        GameObject stackCounter = FindNestedGameObjectByTag(visitedButtons[visitedIndex], Constants.UI_INVENTORY_STACK);
        itemButton = visitedButtons[visitedIndex];

        // Count all existing items so far
        int count = inventoryItems.FindAll(inventoryItem => inventoryItem.name == visited[visitedIndex].name).Count;

        stackCounter.GetComponent<Text>().text = "x"+count;
      }
      else
      {
        // Register item in visited list for stackable cases
        visited.Add(item);

        // Instantiate Item Button
        itemButton = Instantiate(itemButtonPrefab);
        visitedButtons.Add(itemButton);
      }

      // Icon imagery logic
      GameObject sprite = FindNestedGameObjectByTag(itemButton, Constants.UI_INVENTORY_IMAGE);
      if (sprite != null)
      {
        sprite.GetComponent<Image>().sprite = item.icon;
      }

      // Parent item button to panel
      itemButton.transform.SetParent(panel.transform);

      // @ Equippable Item Logic
      if (item.graphic != null)
      {
        // @ If item is already equipped, show Equipped banner
        ScriptableItem equippedItem = equipmentManager.GetEquippedItem(item.slot);

        GameObject equippedBanner = FindNestedGameObjectByTag(itemButton, Constants.UI_INVENTORY_EQUIPPED);
        equippedBanner.SetActive(false);

        if (equippedItem == item)
        {
          // Search for the Equipped Banner and activate it
          equippedBanner?.SetActive(true);

        }

        itemButton.GetComponent<Button>().onClick.AddListener(() => {

          // @Is Item equipped already?
          if (equippedItem == item)
          {
            equipmentManager.Unequip(item.slot);
            equippedBanner?.SetActive(false);
          }
          else
          {
            equipmentManager.Equip(item.slot, item);
            equippedBanner?.SetActive(true);
          }

          // Redraw
          Draw();

        });
      }
    }
  }

  private GameObject FindNestedGameObjectByTag (GameObject parent, string _tag)
  {
    foreach (Transform child in parent.transform)
    {
      if (child.gameObject.tag == _tag)
      {
        return child.gameObject;
      }
    }

    return null;
  }

}
