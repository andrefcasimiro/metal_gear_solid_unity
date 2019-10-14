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

  private int bulletsFired;
  private bool hasReloadedOnce;

  // @Constructor
  public Weapon (GameObject owner, ScriptableWeapon scriptableWeapon, GameObject weaponGameObject)
  {
    this.m_owner = owner;
    this.m_scriptableWeapon = scriptableWeapon;
    this.m_weaponGameObject = weaponGameObject;

    // @ Assign needed components
    audioSource = this.m_weaponGameObject.GetComponent<AudioSource>();
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

    // @ Magazine is empty. We need to reload.
    if (
      !hasReloadedOnce
      || bulletsFired >= m_scriptableWeapon.magazineCapacity
      || (GetBulletsAmount() + (m_scriptableWeapon.magazineCapacity - bulletsFired) <= 0) // Total bullets plus the ones we have in the magazine
     )
    {
      DryShoot();
      return;
    }

    FireBullet();

    DispatchRaycast();

    CalculateAmmunition();
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
    Vector3 origin = m_weaponGameObject.transform.position;
    Vector3 destination = m_weaponGameObject.transform.forward;

    RaycastHit hit;
    if (Physics.Raycast(origin, destination, out hit, m_scriptableWeapon.range))
    {
      GameObject target = hit.transform.gameObject;

      Debug.Log("We shot " + target.name);
    }
  }

  private void CalculateAmmunition ()
  {
    if (bulletsFired < m_scriptableWeapon.magazineCapacity)
    {
      bulletsFired++;
    }
  }

  public void Reload ()
  {
    int bulletsAmount = GetBulletsAmount();

    // @ No magazines remaining for reload
    if (bulletsAmount <= 0)
    {
      // @ Play reload sfx
      audioSource.clip = m_scriptableWeapon.emptySFX;
      audioSource.Play();

      return;
    }

    int amountToRemove = !hasReloadedOnce
      ? m_scriptableWeapon.magazineCapacity
      : bulletsFired;

    // @ Reload and preserve any unfired bullets
    for (int i = 0; i < amountToRemove; i++)
    {
      inventory.Remove(m_scriptableWeapon.bullet);
    }

    int bulletsInTheChamber = m_scriptableWeapon.magazineCapacity - bulletsFired;

    bulletsFired = bulletsAmount + bulletsInTheChamber >= m_scriptableWeapon.magazineCapacity
      ? 0
      : m_scriptableWeapon.magazineCapacity - bulletsInTheChamber - bulletsAmount;

    hasReloadedOnce = true;

    // @ Play reload sfx
    audioSource.clip = m_scriptableWeapon.reloadSFX;
    audioSource.Play();
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

    int magazineCapacity = m_scriptableWeapon.magazineCapacity;
    int bulletsAmount = GetBulletsAmount();

    int current = hasReloadedOnce ? magazineCapacity - bulletsFired : 0;
    int total = bulletsAmount + magazineCapacity >= magazineCapacity ? (bulletsAmount) : 0;

    bulletCounterText.text = current + "/" + total;
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
  private int GetBulletsAmount ()
  {
    return inventory.GetQuantity(m_scriptableWeapon.bullet);
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
