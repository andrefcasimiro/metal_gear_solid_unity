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

  // Private variables
  private int bulletsFired;

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
    }
    else
    {
      bulletsFired = magazineCapacity - currentTotalBullets;
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
      Reload(owner);


      return;
    }
    else
    {
      // Fire one bullet
      inventory.Remove(bullet);
      bulletsFired++;
    }

    // Update UI
    DrawBulletCount(bulletCountUI, inventory.GetQuantity(bullet));

    audioSource.clip = fireSFX;
    audioSource.Play();

    // Play Firing Animation
    owner.GetComponent<Animator>().SetTrigger(Constants.SHOOT);

    // We need to obtain the instance of the gun graphic
    GameObject weaponGraphic = owner.GetComponent<EquipmentManager>().GetEquippedItemGameObject(this.slot);

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

  public void DrawBulletCount (GameObject bulletCountUI, int currentBulletAmount)
  {
    // @ UI Bullet Counting Logic
    Text bulletCounterText = bulletCountUI.GetComponent<Text>();

    if (currentBulletAmount <= magazineCapacity)
    {
      currentBulletAmount = 0;
    }

    bulletCounterText.text = (magazineCapacity - bulletsFired).ToString() + "/" + currentBulletAmount;
  }


}

public enum WeaponType { None, Pistol, Rifle, Sniper, Shotgun, Missile_Launcher, Melee }
