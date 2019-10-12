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
  private bool isActive = false;

  private void Awake ()
  {
    inventory = new Inventory(this.gameObject);

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

    foreach (ScriptableItem item in inventoryItems)
    {
      GameObject itemButton = Instantiate(itemButtonPrefab);
      itemButton.transform.GetChild(0).GetComponent<Image>().sprite = item.icon;
      itemButton.transform.SetParent(panel.transform);

      // @ Equippable Item
      if (item.slot != null && item.graphic != null)
      {
        // @ If item is already equipped, show Equipped banner
        ScriptableItem equippedItem = equipmentManager.GetEquippedItem(item.slot);

        if (equippedItem == item)
        {
          itemButton.GetComponent<EquippedBanner>().Display();
        }

        itemButton.GetComponent<Button>().onClick.AddListener(() => {

          // @Is Item equipped already?
          if (equippedItem == item)
          {
            equipmentManager.Unequip(item.slot);
            itemButton.GetComponent<EquippedBanner>().Hide();
          }
          else
          {
            equipmentManager.Equip(item.slot, item);
            itemButton.GetComponent<EquippedBanner>().Display();
          }

          Draw();
        });
      }
    }
  }

}
