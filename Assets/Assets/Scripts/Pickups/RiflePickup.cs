using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiflePickup : MonoBehaviour
{
  public GameObject rifle;
  public GameObject WeaponHolder;

  private void OnTriggerEnter(Collider other)
  {
    rifle.SetActive(true);
    WeaponHolder.SetActive(true);

    Destroy(gameObject);
  }
}
