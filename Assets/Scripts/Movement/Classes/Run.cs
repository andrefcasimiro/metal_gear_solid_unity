using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Run {

  private GameObject m_owner;

  // @ Constructor
  public Run (GameObject owner)
  {
    this.m_owner = owner;
  }

  public void Listen ()
  {
    Animator animator = m_owner.GetComponent<Animator>();

    animator.SetBool(Constants.IS_RUNNING, Input.GetButton(Constants.RUN));
  }

  // @ AI Methods
  public void Dispatch ()
  {
      Animator animator = m_owner.GetComponent<Animator>();

      animator.SetBool(Constants.IS_RUNNING, true);
  }

  public void Stop ()
  {
      Animator animator = m_owner.GetComponent<Animator>();

      animator.SetBool(Constants.IS_RUNNING, false);
  }
}
