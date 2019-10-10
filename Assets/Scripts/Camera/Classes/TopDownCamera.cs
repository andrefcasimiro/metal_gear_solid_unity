using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TopDownCamera {

  // @Camera owner (player, npc, enemy...)
  private Transform m_target;
  // @Position offset from owner
  private Vector3 m_offset;

  // @Constructor
  public TopDownCamera (Transform target, Vector3 offset) {
    this.m_target = target;
    this.m_offset = offset;
  }

  // @Follow target
  public void FollowTarget (Transform cameraTransform)
  {
    cameraTransform.position = m_target.transform.position + m_offset;
  }
}
