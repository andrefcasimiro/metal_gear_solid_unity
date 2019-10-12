using UnityEngine;

public class GameController : MonoBehaviour {

  public void Resume ()
  {
    Time.timeScale = 1f;
  }

  public void Pause ()
  {
    Time.timeScale = 0f;
  }

  public bool IsPaused ()
  {
    return Time.timeScale == 0f;
  }
}
