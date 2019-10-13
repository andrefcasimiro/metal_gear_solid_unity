using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Weapon : MonoBehaviour
{
  private GameObject m_owner;
  private ScriptableWeapon m_scriptableWeapon;
  private GameObject m_weaponGameObject;

  // @ The timer used as cooldown for the weapon's fire rate
  private Timer fireRateTimer;

  // @ Components
  private AudioSource audioSource;
  private Inventory inventory;
  private Animator animator;

  private float characterHeight = 1f;

  // @Constructor
  public Weapon (GameObject owner, ScriptableWeapon scriptableWeapon, GameObject weaponGameObject)
  {
    this.m_owner = owner;
    this.m_scriptableWeapon = scriptableWeapon;
    this.m_weaponGameObject = weaponGameObject;

    // @ Assign needed components
    audioSource = this.m_owner.GetComponent<AudioSource>();
    inventory = this.m_owner.GetComponent<InventoryManager>().inventory;
    animator = this.m_owner.GetComponent<Animator>();
  }

  public void Shoot ()
  {
    if (!CanShoot())
    {
      return;
    }

    // @ Initialize fire rate timer
    fireRateTimer = new Timer(m_scriptableWeapon.fireRate);

    if (!HasEnoughBullets())
    {
      DryShoot();
      return;
    }

    FireBullet();

    DispatchRaycast();
  }

  // @ Sub Methods
  private void DryShoot ()
  {
    // We've emptied the magazine. Ask for a reload.
    audioSource.clip = m_scriptableWeapon.emptySFX;
    audioSource.Play();
  }

  private void FireBullet ()
  {
    // @ Remove 1 bullet from inventory
    inventory.Remove(m_scriptableWeapon.bullet);

    // @ Play shooting sfx
    audioSource.clip = m_scriptableWeapon.fireSFX;
    audioSource.Play();

    // @ Play firing animation
    animator.SetTrigger(Constants.SHOOT);

    // @ Play muzzleflash fx
    PlayMuzzleflash();
  }

  private void PlayMuzzleflash ()
  {
    foreach (Transform child in m_weaponGameObject.transform)
    {
      if (child.gameObject.tag == Constants.MUZZLEFLASH)
      {
        ParticleSystem particle = child.gameObject.GetComponent<ParticleSystem>();
        particle.Clear();
        particle.Play();
      }
    }
  }

  private void DispatchRaycast ()
  {
    Vector3 origin = new Vector3(m_owner.transform.position.x, m_owner.transform.position.y + characterHeight, m_owner.transform.position.z);
    Vector3 destination = m_owner.transform.forward;

    RaycastHit hit;
    if (Physics.Raycast(origin, destination, out hit, m_scriptableWeapon.range))
    {
      GameObject target = hit.transform.gameObject;

      Debug.Log("We shot " + target.name);
    }
  }

  // @ UI

  public void DrawAmmunitionCounter ()
  {
    Text bulletCounterText = null;

    // Play muzzleflash fx if it exists
    foreach (Transform child in m_weaponGameObject.transform)
    {
      if (child.gameObject.tag == Constants.UI_BULLET_COUNT)
      {
        bulletCounterText = child.transform.GetChild(0).gameObject.GetComponent<Text>();
      }
    }

    bulletCounterText.text = "0/0";
  }

  public void UpdateFireRateTimer ()
  {
    if (fireRateTimer == null)
    {
      return;
    }

    if (fireRateTimer.HasFinished())
    {
      fireRateTimer = null;
    }
    else
    {
      fireRateTimer.Increment();
    }
  }

  // @ Helpers
  private bool HasEnoughBullets ()
  {
    int totalBullets = inventory.GetQuantity(m_scriptableWeapon.bullet);

    return totalBullets >= 0;
  }

  private bool CanShoot ()
  {
    if (fireRateTimer == null)
    {
      return true;
    }

    return (fireRateTimer.HasFinished());
  }
}
