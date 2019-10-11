using UnityEngine;

public class EquippedBanner : MonoBehaviour {

  public GameObject banner;

  private void Awake ()
  {
    banner.SetActive(false);
  }

  public void Display ()
  {
    banner.SetActive(true);
  }

  public void Hide ()
  {
    banner.SetActive(false);
  }
}
