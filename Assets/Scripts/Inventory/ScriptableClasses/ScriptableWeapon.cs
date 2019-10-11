using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Inventory/Weapon", order = 1)]
public class ScriptableWeapon : ScriptableItem {

  [Header("Type")]
  public WeaponType type;

  [Header("Slot")]
  public WeaponSlot slot;

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

  [Header("Graphic")]
  public GameObject graphic;

}

public enum WeaponType { Pistol, Rifle, Sniper, Shotgun, Missile_Launcher, Melee }

public enum WeaponSlot { Left_Hand, Right_Hand }
