using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(EquipmentManager))]
public class WeaponManager : MonoBehaviour {

  private EquipmentManager equipmentManager;
  private GameController gameController;

  // @ Equipped weapons
  private List<Weapon> weapons = new List<Weapon>();

  private void Start ()
  {
    equipmentManager = GetComponent<EquipmentManager>();
    gameController = GameObject.FindWithTag(Constants.GAME_CONTROLLER).GetComponent<GameController>();

    // @Event Subscription
    equipmentManager.EquipEvent.AddListener(UpdateWeaponsList);
    equipmentManager.UnequipEvent.AddListener(UpdateWeaponsList);
  }

  private void UpdateWeaponsList()
  {
    weapons.Clear();

    //@ Left hand slot
    AddWeaponIfExists(EquipmentSlotType.Left_Hand);

    //@ Right hand slot
    AddWeaponIfExists(EquipmentSlotType.Right_Hand);
  }

  private void Update ()
  {
    foreach (Weapon weapon in weapons)
    {
      weapon.DrawAmmunitionCounter();
      weapon.UpdateFireRateTimer();
    }


    // Try reloading
    if (Input.GetButtonDown(Constants.RELOAD))
    {
      foreach (Weapon weapon in weapons)
      {
        weapon.Reload();
      }
    }

    // Try firing
    if (Input.GetButton(Constants.FIRE))
    {
      if (gameController.IsPaused())
      {
        return;
      }

      foreach (Weapon weapon in weapons)
      {
        weapon.Shoot();
      }
    }
  }

  private void AddWeaponIfExists (EquipmentSlotType slotType)
  {
    ScriptableItem item = equipmentManager.GetEquippedItem(slotType);

    if (item is ScriptableWeapon)
    {
      weapons.Add(new Weapon(
        this.gameObject,
        (ScriptableWeapon)item,
        equipmentManager.GetEquippedItemGameObject(slotType)
      ));
    }
  }
}
