using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 0)]
public class ScriptableItem : ScriptableObject {

  new public string name = "New item";
  public Sprite icon = null;
  public bool isDefault = false;
  public ItemType itemType = ItemType.CONSUMABLE;

  [Header("Slot")]
  public EquipmentSlotType slot;

  [Header("Stack Options")]
  public bool stackable;

  [Header("Graphic")]
  public GameObject graphic;

  // @Local transforms applied after instantiating and parenting graphic to owner bone
  [Header("Local Transforms")]
  public Vector3 instantiatedPosition;
  public Quaternion instantiatedRotation;
  public Vector3 instantiatedScale;
}

public enum ItemType {
  CONSUMABLE,
  WEAPON,
}
