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

  // @ Handle any locomotion input and assign it to the animator
  public void Listen ()
  {
    // @ Update animator
    Animator animator = m_owner.GetComponent<Animator>();

    animator.SetBool(Constants.IS_RUNNING, Input.GetButton(Constants.RUN));
  }

  // @ AI Methods
  public void Dispatch ()
  {
      // @ Update animator
      Animator animator = m_owner.GetComponent<Animator>();

      // @ Force AI to Run
      animator.SetBool(Constants.IS_RUNNING, true);
  }

  public void Stop ()
  {
      // @ Update animator
      Animator animator = m_owner.GetComponent<Animator>();

      // @ Force AI to Run
      animator.SetBool(Constants.IS_RUNNING, false);
  }
}
