using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Walk {

  private GameObject m_owner;
  private float rotationSpeed = 10f;

  // @ Constructor
  public Walk (GameObject owner)
  {
    this.m_owner = owner;
  }

  // @ Handle any locomotion input and assign it to the animator
  public void Listen ()
  {
    float vertical = GetVerticalInput();
    float horizontal = GetHorizontalInput();

    // @ Update animator
    Animator animator = m_owner.GetComponent<Animator>();
    animator.SetFloat(Constants.VERTICAL, vertical);
    animator.SetFloat(Constants.HORIZONTAL, horizontal);

    // @ Calculate desired rotation
    if (horizontal != 0f || vertical != 0f) {
      m_owner.transform.rotation = GetDesiredRotation(vertical, horizontal);
    }
  }

  // @ AI Method
  public void Move (float vertical, float horizontal, bool isRunning = false)
  {
      // @ Update animator
      Animator animator = m_owner.GetComponent<Animator>();
      animator.SetFloat(Constants.VERTICAL, vertical);
  }

  // @ Return vertical input
  private float GetVerticalInput ()
  {
    return Input.GetAxis(Constants.VERTICAL);
  }

  // @ Return horizontal input
  private float GetHorizontalInput ()
  {
    return Input.GetAxis(Constants.HORIZONTAL);
  }

  // @ Handles rotation based on input
  private Quaternion GetDesiredRotation (float vertical, float horizontal)
  {
    Vector3 targetDirection = new Vector3(-1 * horizontal, 0f, -1 * vertical);
    Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
    Quaternion desiredRotation = Quaternion.Lerp(m_owner.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    return desiredRotation;
  }
}
