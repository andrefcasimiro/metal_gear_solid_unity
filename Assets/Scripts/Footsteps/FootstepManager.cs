using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


[Serializable]
public class FootstepSound {
  public string key;
  public AudioClip[] clips;
  public LayerMask layer;
}

public class FootstepManager : MonoBehaviour {

  // @Footsteps
  public List<FootstepSound> footsteps = new List<FootstepSound>();

  // @Search by given layer
  public FootstepSound GetByLayer (int layer)
  {
    return footsteps.Find(footstep => Layers.Contains(footstep.layer, layer));
  }

}
