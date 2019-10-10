using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TopDownCameraController : MonoBehaviour {

  public Transform target;
  public Quaternion defaultRotation = Quaternion.Euler(0, 180, 0);
  public Vector3 offset;


  private TopDownCamera topDownCamera;

  private void Awake ()
  {
    topDownCamera = new TopDownCamera(target, offset);

    // @ Rotate to face owner
    this.transform.rotation = defaultRotation;
  }

  private void LateUpdate ()
  {
    topDownCamera.FollowTarget(this.transform);
  }

}
