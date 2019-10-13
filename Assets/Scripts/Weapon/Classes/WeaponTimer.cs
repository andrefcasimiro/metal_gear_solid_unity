using UnityEngine;
using System.Collections;

public class WeaponTimer
{
  public EquipmentSlotType equipmentSlot;
  public Timer timer;

  public WeaponTimer(EquipmentSlotType _equipmentSlot, Timer _timer)
  {
    this.equipmentSlot = _equipmentSlot;
    this.timer = _timer;
  }
}
