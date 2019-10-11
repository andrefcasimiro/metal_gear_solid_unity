using UnityEngine;

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

  // @ Shoot Method
  public void Shoot (GameObject owner)
  {
    AudioSource audioSource = owner.GetComponent<AudioSource>();

    audioSource.clip = fireSFX;
    audioSource.Play();

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
}

public enum WeaponType { None, Pistol, Rifle, Sniper, Shotgun, Missile_Launcher, Melee }
