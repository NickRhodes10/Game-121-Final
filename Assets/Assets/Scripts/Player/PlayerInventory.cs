using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
  public static PlayerInventory instance;

  public List<string> keycards;

  private void Awake()
  {
    if (PlayerInventory.instance = null)
      PlayerInventory.instance = this;
    else if (PlayerInventory.instance != this)
      Destroy(this);
  }

  public bool CheckForKey(string keyToCheck)
  {
    if (keycards.Contains(keyToCheck))
      return true;
    else
      return false;
  }

  public void AddKey(string keyToAdd)
  {
    keycards.Add(keyToAdd);
    Debug.Log("The key" + keyToAdd + "has been added to the players inventory.");
  }
}
