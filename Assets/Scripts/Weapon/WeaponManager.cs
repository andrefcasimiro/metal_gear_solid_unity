using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class WeaponTimer {
  public EquipmentSlotType equipmentSlot;
  public Timer timer;

  public WeaponTimer (EquipmentSlotType _equipmentSlot, Timer _timer)
  {
    this.equipmentSlot = _equipmentSlot;
    this.timer = _timer;
  }
}

[RequireComponent(typeof(EquipmentManager))]
public class WeaponManager : MonoBehaviour {

  private EquipmentManager equipmentManager;
  private GameController gameController;

  private ScriptableWeapon leftHandWeapon;
  private ScriptableWeapon rightHandWeapon;

  // If we want dual wielding, we should make timer a list so we don't override timers between weapons
  private List<WeaponTimer> weaponTimers = new List<WeaponTimer>();

  private GameObject bulletCount;

  private void Start ()
  {
    equipmentManager = GetComponent<EquipmentManager>();
    gameController = GameObject.FindWithTag(Constants.GAME_CONTROLLER).GetComponent<GameController>();
    bulletCount = GameObject.FindWithTag(Constants.UI_BULLET_COUNT);
  }

  private void Update ()
  {

    HandleTimers();

    // Try firing
    if (Input.GetButton(Constants.FIRE))
    {
      if (gameController.IsPaused())
      {
        return;
      }

      // @Any weapons equipped?
      leftHandWeapon = (ScriptableWeapon)equipmentManager.GetEquippedItem(EquipmentSlotType.Left_Hand);
      rightHandWeapon = (ScriptableWeapon)equipmentManager.GetEquippedItem(EquipmentSlotType.Right_Hand);

      // Left Hand Weapon Handler
      if (
        leftHandWeapon != null
        && CanShoot(EquipmentSlotType.Left_Hand)
      )
      {
        leftHandWeapon.Shoot(this.gameObject, bulletCount);

        Timer timer = new Timer(leftHandWeapon.fireRate);
        WeaponTimer weaponTimer = new WeaponTimer(EquipmentSlotType.Left_Hand, timer);
        weaponTimers.Add(weaponTimer);
      }

      // Right Hand Weapon Handler
      if (
        rightHandWeapon != null
        && CanShoot(EquipmentSlotType.Right_Hand)
      )
      {
        rightHandWeapon.Shoot(this.gameObject, bulletCount);

        Timer timer = new Timer(rightHandWeapon.fireRate);
        WeaponTimer weaponTimer = new WeaponTimer(EquipmentSlotType.Right_Hand, timer);
        weaponTimers.Add(weaponTimer);
      }

    }
  }

  // @ Check for each slot if timer allows the next shot
  private bool CanShoot (EquipmentSlotType weaponSlot)
  {
    if (weaponTimers.Count <= 0)
    {
      return true;
    }

    foreach (WeaponTimer weaponTimer in weaponTimers)
    {
      if (weaponTimer.equipmentSlot == weaponSlot)
      {
        return (bool)(weaponTimer.timer != null && weaponTimer.timer.HasFinished());
      }
    }

    // If we don't find a timer match, allow shooting
    return true;
  }

  private void HandleTimers ()
  {
    if (weaponTimers.Count > 0)
    {
      foreach (WeaponTimer weaponTimer in weaponTimers)
      {
        if (weaponTimer.timer != null)
        {
          // If timer has finished, remove it
          if (weaponTimer.timer.HasFinished())
          {
            weaponTimers.Remove(weaponTimer);
            break;
          }
          else
          {
            // Else, increment it until it finishes
            weaponTimer.timer.Increment();
          }
        }
      }
    }
  }

}
