using UnityEngine;

public class EquipmentManager : MonoBehaviour {

  [Header("Transforms")]
  public Transform leftHand;
  public Transform rightHand;

  private EquipmentSlot leftHandEquipmentSlot;
  private EquipmentSlot rightHandEquipmentSlot;

  void Awake ()
  {
    leftHandEquipmentSlot = new EquipmentSlot(EquipmentSlotType.Left_Hand, leftHand);
    rightHandEquipmentSlot = new EquipmentSlot(EquipmentSlotType.Right_Hand, rightHand);
  }

  public void Equip (EquipmentSlotType slotType, ScriptableItem itemToEquip)
  {
    if (IsLeftHand(slotType))
    {
      leftHandEquipmentSlot.Equip(itemToEquip);
    }

    if (IsRightHand(slotType))
    {
      rightHandEquipmentSlot.Equip(itemToEquip);
    }
  }

  public void Unequip (EquipmentSlotType slotType)
  {
    if (IsLeftHand(slotType))
    {
      leftHandEquipmentSlot.Unequip();
    }

    if (IsRightHand(slotType))
    {
      rightHandEquipmentSlot.Unequip();
    }
  }

  public ScriptableItem GetEquippedItem (EquipmentSlotType slotType) {
    if (IsLeftHand(slotType))
    {
      return leftHandEquipmentSlot.GetEquippedItem();
    }

    if (IsRightHand(slotType))
    {
      return rightHandEquipmentSlot.GetEquippedItem();
    }

    return null;
  }

  private bool IsLeftHand (EquipmentSlotType slotType) {
    return slotType.ToString() == Constants.LEFT_HAND;
  }
  private bool IsRightHand (EquipmentSlotType slotType) {
    return slotType.ToString() == Constants.RIGHT_HAND;
  }
}
