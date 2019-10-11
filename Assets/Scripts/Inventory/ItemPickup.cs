using UnityEngine;

public class ItemPickup : MonoBehaviour {

  public ScriptableItem item;

  private void Update () {
    this.transform.Rotate(0, 100f * Time.deltaTime, 0);
  }

  private void OnTriggerEnter (Collider collider)
  {
    InventoryManager inventoryManager = collider.gameObject.GetComponent<InventoryManager>();
    if (inventoryManager != null)
    {
      // Add item to game object inventory
      inventoryManager.inventory.Add(item);
      Debug.Log(inventoryManager.inventory.ListAll());
    }

    Destroy(this.gameObject);
  }

}
