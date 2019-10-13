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

  [Header("Reload")]
  public bool autoReload;

}

public enum WeaponType { None, Pistol, Rifle, Sniper, Shotgun, Missile_Launcher, Melee }
