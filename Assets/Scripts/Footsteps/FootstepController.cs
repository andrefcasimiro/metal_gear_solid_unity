using UnityEngine;

public class FootstepController : MonoBehaviour {

  // @ References to attach the footstep detector to
  public Transform leftFoot;
  public Transform rightFoot;

  // @ Prefab
  public GameObject footstepDetector;

  // @ Instantiated prefab references
  private GameObject leftFootstepDetector;
  private GameObject rightFootstepDetector;

  // @ Classes
  private Footstep footstep;

  private void Start ()
  {
    // @Transform setups
    leftFootstepDetector = Instantiate(footstepDetector, Vector3.zero, Quaternion.identity);
    rightFootstepDetector = Instantiate(footstepDetector, Vector3.zero, Quaternion.identity);
    leftFootstepDetector.transform.SetParent(leftFoot);
    leftFootstepDetector.transform.localPosition = new Vector3(0f, -0.1f, 0f);
    rightFootstepDetector.transform.SetParent(rightFoot);
    rightFootstepDetector.transform.localPosition = new Vector3(0f, -0.1f, 0f);

    // @Subscribe to footstep triggering on the ground layers
    leftFootstepDetector.GetComponent<FootstepTrigger>().DispatchEvent.AddListener(PlayLeftFootstep);
    rightFootstepDetector.GetComponent<FootstepTrigger>().DispatchEvent.AddListener(PlayRightFootstep);

    // @Initialize Footstep Class
    footstep = new Footstep (
      // @The footstep manager soundbank
      GameObject.FindWithTag(Constants.FOOTSTEP_MANAGER).GetComponent<FootstepManager>(),
      // @An arbitrary 3D audio source, either from the left or right foot is fine
      leftFootstepDetector.GetComponent<AudioSource>()
    );
  }

  private void PlayLeftFootstep ()
  {
    int currentLayer = leftFootstepDetector.GetComponent<FootstepTrigger>().GetCurrentLayer();

    footstep.Dispatch(currentLayer);
  }

  private void PlayRightFootstep ()
  {
    int currentLayer = rightFootstepDetector.GetComponent<FootstepTrigger>().GetCurrentLayer();

    footstep.Dispatch(currentLayer);
  }


}