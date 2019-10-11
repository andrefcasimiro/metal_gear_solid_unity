using UnityEngine;
using UnityEngine.Events;

public class FootstepTrigger : MonoBehaviour {

  [HideInInspector]
  public UnityEvent DispatchEvent;

  public LayerMask layers;

  private int currentLayer;

  private void OnTriggerEnter (Collider collider)
  {
    if (Layers.Contains(layers, collider.gameObject.layer))
    {
      currentLayer = collider.gameObject.layer;
      DispatchEvent.Invoke();
    }
  }

  public int GetCurrentLayer ()
  {
    return currentLayer;
  }
}
