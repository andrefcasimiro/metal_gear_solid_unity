using UnityEngine;

public class InventoryManager : MonoBehaviour {

  [HideInInspector]
  public Inventory inventory;

  private void Awake ()
  {
    inventory = new Inventory(this.gameObject);
  }


}