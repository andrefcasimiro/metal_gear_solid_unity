using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Inventory/Bullet", order = 2)]
public class ScriptableBullet : ScriptableItem {

  public BulletType type;

}

public enum BulletType { Pistol, Rifle, Sniper, Shotgun, Missile_Launcher }

