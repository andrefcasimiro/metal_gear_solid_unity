using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Locomotion {

  private Animator _animator;

  // @ Constructor
  public Locomotion (Animator animator)
  {
    this._animator = animator;
  }

  // @ Handle any locomotion input and assign it to the animator
  public void Listen ()
  {
    _animator.SetFloat(Constants.VERTICAL, GetVerticalInput());
    _animator.SetFloat(Constants.HORIZONTAL, GetHorizontalInput());
  }

  // @ Return vertical input
  public float GetVerticalInput ()
  {
    return Input.GetAxis(Constants.VERTICAL);
  }

  // @ Return horizontal input
  public float GetHorizontalInput ()
  {
    return Input.GetAxis(Constants.VERTICAL);
  }

  // @ AI Method - Move animator manually
  public void Move (float vertical = 0f, float horizontal = 0f)
  {
    _animator.SetFloat(Constants.VERTICAL, vertical);
    _animator.SetFloat(Constants.HORIZONTAL, horizontal);
  }
}
