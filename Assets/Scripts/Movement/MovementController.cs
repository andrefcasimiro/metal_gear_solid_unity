using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour {

  private Walk walk;
  private Run run;
  private Crouch crouch;
  private Prone prone;

  private GameController gameController;

  private void Awake ()
  {
    walk = new Walk(this.gameObject);
    run = new Run(this.gameObject);
    crouch = new Crouch(this.gameObject);
    prone = new Prone(this.gameObject);
  }

  private void Start ()
  {
    gameController = GameObject.FindWithTag(Constants.GAME_CONTROLLER).GetComponent<GameController>();
  }

  private void Update ()
  {
    if (gameController.IsPaused())
    {
      return;
    }

    walk.Listen();
    run.Listen();
    crouch.Listen();
    prone.Listen();
  }
}
