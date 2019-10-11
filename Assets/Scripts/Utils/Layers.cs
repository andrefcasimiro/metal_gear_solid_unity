using UnityEngine;

public class Layers : MonoBehaviour {

  public static bool Contains(LayerMask mask, int layer)
  {
      return mask == (mask | (1 << layer));
  }

}