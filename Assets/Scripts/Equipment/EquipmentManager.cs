using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Slot {
  public EquipmentSlotType type;
  public EquipmentSlot equipmentSlot;

  public Slot (EquipmentSlotType _type, EquipmentSlot _equipmentSlot)
  {
    this.type = _type;
    this.equipmentSlot = _equipmentSlot;
  }
}

public class EquipmentManager : MonoBehaviour {

  [Header("Transforms")]
  public Transform leftHand;
  public Transform rightHand;

  private readonly List<Slot> slots = new List<Slot>();


  void Awake ()
  {
    EquipmentSlot leftHandEquipmentSlot = new EquipmentSlot(EquipmentSlotType.Left_Hand, leftHand, this.gameObject);
    EquipmentSlot rightHandEquipmentSlot = new EquipmentSlot(EquipmentSlotType.Right_Hand, rightHand, this.gameObject);

    // List
    Slot leftHandSlot = new Slot(EquipmentSlotType.Left_Hand, leftHandEquipmentSlot);
    Slot rightHandSlot = new Slot(EquipmentSlotType.Right_Hand, rightHandEquipmentSlot);

    slots.Add(leftHandSlot);
    slots.Add(rightHandSlot);
  }

  public void Equip (EquipmentSlotType slotType, ScriptableItem itemToEquip)
  {
    GetSlot(slotType).equipmentSlot.Equip(itemToEquip);
    
    // Animator Updates
    GetComponent<Animator>().SetBool(Constants.IS_DUAL_WIELDING, IsDualWielding());
  }

  public void Unequip (EquipmentSlotType slotType)
  {
    EquipmentSlot slot = GetSlot(slotType).equipmentSlot;

    slot.Unequip();
  
    // Animator Updates
    GetComponent<Animator>().SetBool(Constants.IS_DUAL_WIELDING, IsDualWielding());
  }

  public ScriptableItem GetEquippedItem (EquipmentSlotType slotType) {
    return GetSlot(slotType).equipmentSlot.GetEquippedItem();
  }

  public GameObject GetEquippedItemGameObject (EquipmentSlotType slotType) {
    return GetSlot(slotType).equipmentSlot.GetEquippedItemGameObject();
  }

  public Slot GetSlot (EquipmentSlotType slotType)
  {
    Slot slot = slots.Find(entry => entry.type == slotType);

    if (slot != null)
    {
      return slot;
    }

    return null;
  }

  private bool IsDualWielding ()
  {
    return (
      GetEquippedItem(EquipmentSlotType.Right_Hand) != null
      && GetEquippedItem(EquipmentSlotType.Left_Hand) != null
    );

  }
}
