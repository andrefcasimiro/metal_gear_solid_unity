using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Prone {

  private GameObject m_owner;

  private float defaultHeight;
  private Vector3 defaultCenter;

  private bool isProned;

  // @ Constructor
  public Prone (GameObject owner)
  {
    this.m_owner = owner;
    defaultHeight = owner.GetComponent<CapsuleCollider>().height;
    defaultCenter = owner.GetComponent<CapsuleCollider>().center;
  }

  public void Listen ()
  {
    Animator animator = m_owner.GetComponent<Animator>();
    CapsuleCollider capsuleCollider = m_owner.GetComponent<CapsuleCollider>();

    if (isProned)
    {
      capsuleCollider.height = defaultHeight / 4;
      capsuleCollider.center = new Vector3 (defaultCenter.x, defaultCenter.y / 4, defaultCenter.z);
    }

    if (Input.GetButtonDown(Constants.PRONE) && animator.GetBool(Constants.IS_CROUCHING))
    {
      isProned = !isProned;
    }

    animator.SetBool(Constants.IS_PRONING, isProned);
  }

  // @ AI Methods
  public void Dispatch ()
  {
    Animator animator = m_owner.GetComponent<Animator>();

    animator.SetBool(Constants.IS_PRONING, true);
  }

  public void Stop ()
  {
    Animator animator = m_owner.GetComponent<Animator>();

    animator.SetBool(Constants.IS_PRONING, false);
  }
}
