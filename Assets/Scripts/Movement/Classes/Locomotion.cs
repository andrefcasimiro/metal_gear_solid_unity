using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Locomotion {

  private GameObject m_owner;
  private float m_rotationSpeed;
  private Quaternion cachedRotation;

  // @ Constructor
  public Locomotion (GameObject owner, float rotationSpeed)
  {
    this.m_owner = owner;
    this.m_rotationSpeed = rotationSpeed;
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
    m_owner.transform.rotation = GetDesiredRotation(vertical, horizontal);
  }

  // @ Return vertical input
  public float GetVerticalInput ()
  {
    return Input.GetAxis(Constants.VERTICAL);
  }

  // @ Return horizontal input
  public float GetHorizontalInput ()
  {
    return Input.GetAxis(Constants.HORIZONTAL);
  }

  // @ Handles rotation based on input
  private Quaternion GetDesiredRotation (float vertical, float horizontal)
  {
    Vector3 targetDirection = new Vector3(-1 * horizontal, 0f, -1 * vertical);
    Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
    Quaternion desiredRotation = Quaternion.Lerp(m_owner.transform.rotation, targetRotation, m_rotationSpeed * Time.deltaTime);

    // @ Stores last rotation and applies it to the idle state
    if (horizontal != 0f || vertical != 0f)
    {
      cachedRotation = desiredRotation;
    }
    else
    {
      return cachedRotation;
    }

    return desiredRotation;
  }
}
