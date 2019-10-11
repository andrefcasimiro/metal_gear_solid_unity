using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipmentSlot {

  // @ The equipment slot type
  private EquipmentSlotType m_slot;
  // @ The equipment slot bone target of the owner
  private Transform m_boneReference;

  private ScriptableItem equippedItem = null;
  private GameObject equippedItemGameObject = null;

  public EquipmentSlot (EquipmentSlotType slot, Transform boneReference)
  {
    this.m_slot = slot;
    this.m_boneReference = boneReference;
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
  }

  public void Unequip ()
  {
    equippedItem = null;
    GameObject.Destroy(equippedItemGameObject);
  }

  public ScriptableItem GetEquippedItem ()
  {
    return equippedItem;
  }

}

public enum EquipmentSlotType { Left_Hand, Right_Hand };
