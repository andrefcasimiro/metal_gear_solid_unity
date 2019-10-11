using UnityEngine;

public class Weapon {

  private GameObject m_owner;
  private ScriptableWeapon m_weapon;

  private Animator animator;
  private Inventory inventory;

  public Weapon (GameObject owner, ScriptableWeapon weapon)
  {
    this.m_owner = owner;
    this.m_weapon = weapon;
  }

  public void Shoot ()
  {

    RaycastHit hit;

    if (Physics.Raycast(m_owner.transform.position, m_owner.transform.forward, out hit, m_weapon.range))
    {
      Debug.Log("Hit: " + hit);
    }

  }
}
