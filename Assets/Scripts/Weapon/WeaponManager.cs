using UnityEngine;

[RequireComponent(typeof(EquipmentManager))]
public class WeaponManager : MonoBehaviour {

  private EquipmentManager equipmentManager;
  private GameController gameController;

  private ScriptableWeapon leftHandWeapon;
  private ScriptableWeapon rightHandWeapon;

  private Timer timer;

  private void Start ()
  {
    equipmentManager = GetComponent<EquipmentManager>();
    gameController = GameObject.FindWithTag(Constants.GAME_CONTROLLER).GetComponent<GameController>();
  }

  private void Update ()
  {

    HandleTimer();

    // Try firing
    if (Input.GetButton(Constants.FIRE))
    {
      // If we have a timer and it hasn't finished yet, we shouldn't be able to shoot the weapon
      if (timer != null && !timer.HasFinished() || gameController.IsPaused())
      {
        return;
      }

      // @Any weapons equipped?
      leftHandWeapon = (ScriptableWeapon)equipmentManager.GetEquippedItem(EquipmentSlotType.Left_Hand);
      rightHandWeapon = (ScriptableWeapon)equipmentManager.GetEquippedItem(EquipmentSlotType.Right_Hand);

      // Left Hand Weapon Handler
      if (leftHandWeapon != null && leftHandWeapon.weaponType != null)
      {
        leftHandWeapon.Shoot(this.gameObject);

        timer = new Timer(leftHandWeapon.fireRate);
      }

      // Right Hand Weapon Handler
      if (rightHandWeapon != null && rightHandWeapon.weaponType != null)
      {
        rightHandWeapon.Shoot(this.gameObject);

        timer = new Timer(rightHandWeapon.fireRate);
      }

    }
  }

  private void HandleTimer ()
  {
    // If we have an active timer running, increment it
    if (timer != null)
    {
      timer.Increment();
    }

    // Timer has ended, we don't need it anymore
    if (timer != null && timer.HasFinished())
    {
      timer = null;
    }
  }

}
