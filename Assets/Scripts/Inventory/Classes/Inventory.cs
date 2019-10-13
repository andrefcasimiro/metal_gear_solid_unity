using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Inventory {

  private List<ScriptableItem> items = new List<ScriptableItem>();

  public void Add (ScriptableItem item)
  {
    items.Add(item);
  }

  public void Remove (ScriptableItem item)
  {
    items.Remove(item);
  }

  public int GetQuantity (ScriptableItem item)
  {
    return items.FindAll(entry => entry.name == item.name).Count;
  }

  public List<ScriptableItem> ListAll ()
  {
    return items;
  }
}
