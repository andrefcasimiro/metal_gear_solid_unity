using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TopDownCamera {

  // @Camera owner (player, npc, enemy...)
  private Transform _target;
  // @Position offset from owner
  private Vector3 _offset;

  // @Constructor
  public TopDownCamera (Transform target, Vector3 offset) {
    this._target = target;
    this._offset = offset;
  }

  // @Follow target
  public void FollowTarget (Transform cameraTransform)
  {
    cameraTransform.position = _target.transform.position + _offset;
  }
}
