using UnityEngine;

[RequireComponent(typeof(EquipmentManager))]
public class WeaponManager : MonoBehaviour {

  EquipmentManager equipmentManager;

  private ScriptableWeapon leftHandWeapon;
  private ScriptableWeapon rightHandWeapon;

  private Timer timer;

  private void Start ()
  {
    equipmentManager = GetComponent<EquipmentManager>();
  }

  private void Update ()
  {
    if (timer != null)
    {
      timer.Increment();
    }

    if (Input.GetButton(Constants.FIRE))
    {
      if (timer != null && !timer.HasFinished())
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

}