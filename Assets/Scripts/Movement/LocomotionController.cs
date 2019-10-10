using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class LocomotionController : MonoBehaviour {

  public float rotationSpeed = 10f;

  private Locomotion locomotion;

  private void Awake ()
  {
    locomotion = new Locomotion(
      this.gameObject,
      rotationSpeed
    );
  }

  private void Update ()
  {
    locomotion.Listen();
  }
}
