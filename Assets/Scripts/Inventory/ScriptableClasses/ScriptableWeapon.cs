using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Inventory/Weapon", order = 1)]
public class ScriptableWeapon : ScriptableItem {

  [Header("Type")]
  public WeaponType weaponType;

  [Header("Stats")]
  public float damage = 10f;
  public float range = 35f;

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
    RaycastHit hit;

    if (Physics.Raycast(owner.transform.position, owner.transform.forward, out hit, range))
    {
      Debug.Log("Hit: " + hit);
    }

  }
}

public enum WeaponType { Pistol, Rifle, Sniper, Shotgun, Missile_Launcher, Melee }
