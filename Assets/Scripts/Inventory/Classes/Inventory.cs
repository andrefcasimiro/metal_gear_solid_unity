using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Inventory {

  private List<ScriptableItem> items = new List<ScriptableItem>();
  private GameObject m_owner;

  public Inventory (GameObject owner)
  {
    this.m_owner = owner;
  }

  public void Add (ScriptableItem item)
  {
    items.Add(item);
  }

  public void Remove (ScriptableItem item)
  {
    items.Remove(item);
  }

  public List<ScriptableItem> ListAll ()
  {
    return items;
  }
}
