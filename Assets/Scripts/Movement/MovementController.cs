using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour {

  private Walk walk;
  private Run run;

  private void Awake ()
  {
    walk = new Walk(this.gameObject);
    run = new Run(this.gameObject);
  }

  private void Update ()
  {
    walk.Listen();
    run.Listen();
  }
}
