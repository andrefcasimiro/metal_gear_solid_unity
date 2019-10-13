using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Weapon", menuName = "Inventory/Weapon", order = 1)]
public class ScriptableWeapon : ScriptableItem {

  [Header("Type")]
  public WeaponType weaponType;

  [Header("Stats")]
  public float damage = 10f;
  public float range = 35f;
  public float fireRate = 10f;

  [Header("Ammunition")]
  public ScriptableBullet bullet;
  public int magazineCapacity = 12;

  [Header("Sound")]
  public AudioClip fireSFX;
  public AudioClip emptySFX;
  public AudioClip reloadSFX;

  [Header("Reload")]
  public bool autoReload;

  // Private variables
  private int bulletsFired = -1;
  private int totalBulletsAmount = -1;

  public void Reload (GameObject owner)
  {
    Inventory inventory = owner.GetComponent<InventoryManager>().inventory;
    int currentTotalBullets = inventory.GetQuantity(bullet);

    if (currentTotalBullets <= 0)
    {
      Debug.Log("No ammo left");
      return;
    }

    if (currentTotalBullets >= magazineCapacity)
    {
      bulletsFired = 0;
      totalBulletsAmount = currentTotalBullets - magazineCapacity;
    }
    else
    {
      // No magazines left
      bulletsFired = magazineCapacity - currentTotalBullets;
      totalBulletsAmount = 0;
    }
  }

  // @ Shoot Method
  public void Shoot (GameObject owner, GameObject bulletCountUI = null)
  {
    AudioSource audioSource = owner.GetComponent<AudioSource>();
    Inventory inventory = owner.GetComponent<InventoryManager>().inventory;

    // Handle bullet logic
    if (bulletsFired >= magazineCapacity)
    {
      // We've emptied the magazine. Ask for a reload.
      audioSource.clip = emptySFX;
      audioSource.Play();

      // Reload Automatically
      if (autoReload)
      {
        Reload(owner);
      }

      return;
    }
    else
    {
      // Fire one bullet
      inventory.Remove(bullet);
      bulletsFired++;
    }

    audioSource.clip = fireSFX;
    audioSource.Play();

    // Play Firing Animation
    owner.GetComponent<Animator>().SetTrigger(Constants.SHOOT);

    // We need to obtain the instance of the gun graphic
    GameObject weaponGraphic = GetWeaponInstance(owner);

    // Play muzzleflash fx if it exists
    foreach (Transform child in weaponGraphic.transform)
    {
      if (child.gameObject.tag == Constants.MUZZLEFLASH)
      {
        ParticleSystem particle = child.gameObject.GetComponent<ParticleSystem>();
        particle.Clear();
        particle.Play();
      }
    }

    RaycastHit hit;
    if (Physics.Raycast(
      new Vector3(
        owner.transform.position.x,
        owner.transform.position.y + 1f,
        owner.transform.position.z
      ), owner.transform.forward, out hit, range))
    {
      GameObject target = hit.transform.gameObject;

    }
  }

  public void DrawBulletCount (GameObject owner)
  {
    GameObject weaponGraphic = GetWeaponInstance(owner);
    Text bulletCounterText = null;

    // Play muzzleflash fx if it exists
    foreach (Transform child in weaponGraphic.transform)
    {
      if (child.gameObject.tag == Constants.UI_BULLET_COUNT)
      {
        bulletCounterText = child.transform.GetChild(0).gameObject.GetComponent<Text>();
      }
    }

    // @ UI Bullet Counting Logic
    if (bulletCounterText != null)
    {
      if (bulletsFired == -1)
      {
        bulletsFired = magazineCapacity;
      }

      DrawText(owner, bulletCounterText);
    }
    else
    {
      Debug.Log("Could not find a bullet counter text in the weapon graphic");
    }
  }

  public void DrawText (GameObject owner, Text bulletCounterText)
  {
    Inventory inventory = owner.GetComponent<InventoryManager>().inventory;
    int currentTotalBullets = inventory.GetQuantity(bullet);

    int current;
    int total;

    // @ Initial case when we might not even have bullets
    if (bulletsFired == -1 && totalBulletsAmount == -1)
    {
      if (currentTotalBullets >= magazineCapacity)
      {
        current = magazineCapacity;
        total = currentTotalBullets - magazineCapacity;
      }
      else
      {
        current = currentTotalBullets;
        total = 0;
      }
    }
    else
    {
      current = magazineCapacity - bulletsFired;
      total = currentTotalBullets - magazineCapacity;
    }


    bulletCounterText.text = current + "/" + total;
  }

  private GameObject GetWeaponInstance (GameObject owner)
  {
    GameObject weaponGraphic = owner.GetComponent<EquipmentManager>().GetEquippedItemGameObject(this.slot);

    return weaponGraphic;
  }
}

public enum WeaponType { None, Pistol, Rifle, Sniper, Shotgun, Missile_Launcher, Melee }
