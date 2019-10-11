using UnityEngine;

public class ItemPickup : MonoBehaviour {

  public ScriptableItem item;
  public int quantity = 1;

  private void Update () {
    this.transform.Rotate(0, 100f * Time.deltaTime, 0);
  }

  private void OnTriggerEnter (Collider collider)
  {
    InventoryManager inventoryManager = collider.gameObject.GetComponent<InventoryManager>();
    if (inventoryManager != null)
    {
      // Add item to game object inventory
      for (int i = 0; i < quantity; i++) {
        inventoryManager.inventory.Add(item);
      }
    }

    Destroy(this.gameObject);
  }

}
