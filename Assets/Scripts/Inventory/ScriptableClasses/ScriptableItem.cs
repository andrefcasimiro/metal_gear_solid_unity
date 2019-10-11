using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 0)]
public class ScriptableItem : ScriptableObject {

  new public string name = "New item";
  public Sprite icon = null;
  public bool isDefault = false;

}
