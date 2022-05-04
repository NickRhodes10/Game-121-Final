using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningDoor : MonoBehaviour
{
  [SerializeField] string RequiredID;

  public void OnTriggerEnter(Collider other)
  {
    if(other.tag == "Player" && PlayerInventory.instance.CheckForKey(RequiredID))
    {
      EndGame();
    }
  }

  void EndGame()
  {
    Debug.Log("Win!");
  }
}
