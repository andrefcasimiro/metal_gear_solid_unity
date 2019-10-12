using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipmentSlot {

  // @ The equipment slot type
  private EquipmentSlotType m_slot;
  // @ The equipment slot bone target of the owner
  private Transform m_boneReference;
  // @ The owner of the equipment
  private GameObject m_owner;

  private ScriptableItem equippedItem = null;
  private GameObject equippedItemGameObject = null;

  public EquipmentSlot (EquipmentSlotType slot, Transform boneReference, GameObject owner)
  {
    this.m_slot = slot;
    this.m_boneReference = boneReference;
    this.m_owner = owner;
  }

  public void Equip (ScriptableItem itemToEquip)
  {
    // Instantiate graphic
    equippedItemGameObject = GameObject.Instantiate(itemToEquip.graphic);

    // Attach to parent bone
    equippedItemGameObject.transform.SetParent(m_boneReference);

    // Apply local position, rotation and scale
    equippedItemGameObject.transform.localPosition = itemToEquip.instantiatedPosition;
    equippedItemGameObject.transform.localEulerAngles = new Vector3(
        itemToEquip.instantiatedRotation.x,
        itemToEquip.instantiatedRotation.y,
        itemToEquip.instantiatedRotation.z
    );
    equippedItemGameObject.transform.localScale = itemToEquip.instantiatedScale;

    equippedItem = itemToEquip;

    // @ If equipment is a weapon, update Weapon_ID parameter
    ScriptableWeapon weapon = (ScriptableWeapon)equippedItem;
    if (weapon != null && weapon.weaponType != null)
    {
      m_owner.GetComponent<Animator>().SetInteger(Constants.WEAPON_ID, (int)weapon.weaponType);
    }
  }


  public void Unequip ()
  {
    // @ If equipment to unequip is a weapon, update Weapon_ID parameter
    ScriptableWeapon weapon = (ScriptableWeapon)equippedItem;
    if (weapon != null)
    {
      // We should guarantee that both hands dont have a weapon equipped before doing this:
      // If we unequip a pistol and have a pistol on the other hand, we will run into issues.
      Animator animator = m_owner.GetComponent<Animator>();

      if (!animator.GetBool(Constants.IS_DUAL_WIELDING)) {
        animator.SetInteger(Constants.WEAPON_ID, 0);
      }
    }

    equippedItem = null;
    GameObject.Destroy(equippedItemGameObject);
  }

  public ScriptableItem GetEquippedItem ()
  {
    return equippedItem;
  }

  public GameObject GetEquippedItemGameObject ()
  {
    return equippedItemGameObject;
  }

}

public enum EquipmentSlotType { Left_Hand, Right_Hand };
