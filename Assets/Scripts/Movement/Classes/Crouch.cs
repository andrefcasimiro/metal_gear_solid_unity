using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Crouch {

  private GameObject m_owner;

  private float defaultHeight;
  private Vector3 defaultCenter;

  private bool isCrouched;

  // @ Constructor
  public Crouch (GameObject owner)
  {
    this.m_owner = owner;
    defaultHeight = owner.GetComponent<CapsuleCollider>().height;
    defaultCenter = owner.GetComponent<CapsuleCollider>().center;
  }

  public void Listen ()
  {
    Animator animator = m_owner.GetComponent<Animator>();
    CapsuleCollider capsuleCollider = m_owner.GetComponent<CapsuleCollider>();

    if (isCrouched)
    {
      capsuleCollider.height = defaultHeight / 2;
      capsuleCollider.center = new Vector3 (defaultCenter.x, defaultCenter.y / 2, defaultCenter.z);
    }
    else
    {
      capsuleCollider.height = defaultHeight;
      capsuleCollider.center = defaultCenter;
    }

    if (Input.GetButtonDown(Constants.CROUCH))
    {
      isCrouched = !isCrouched;
    }

    animator.SetBool(Constants.IS_CROUCHING, isCrouched);
  }

  // @ AI Methods
  public void Dispatch ()
  {
    Animator animator = m_owner.GetComponent<Animator>();

    animator.SetBool(Constants.IS_CROUCHING, true);
  }

  public void Stop ()
  {
    Animator animator = m_owner.GetComponent<Animator>();

    animator.SetBool(Constants.IS_CROUCHING, false);
  }
}
