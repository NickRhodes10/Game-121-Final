using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
  public float damage = 50f;
  public float range = 100f;
  public float impactForce = 30f;
  public float fireRate = 15f;

  public Camera fpsCam;
  public GameObject Enemy;
  public GameObject Enemy1;
  public GameObject Enemy2;
  public GameObject Enemy3;
  public GameObject Enemy4;

  private float nextTimeToFire = 0f;


  // Update is called once per frame
  void Update()
  {
    //chcek for user input
    if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
    {
      nextTimeToFire = Time.time + 1f / fireRate;
      Shoot();
    }
  }


  void Shoot()
  {

    //shoot
    Ray ray;
    RaycastHit hit;
    if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
    {
      Transform objectHit = hit.transform;


      Debug.Log(hit.transform.name);

      //create array to iterate through scrips and use the TakeDamage function from IDamage

      if(hit.rigidbody.tag == "Enemy")
      {
        BaseEnemy.currentHealth -= 50;
        if (BaseEnemy.currentHealth <= 0)
        {
          Destroy(Enemy);
          BaseEnemy.currentHealth = 100;
        }
      }

      if (hit.rigidbody.tag == "Enemy")
      {
        BaseEnemy.currentHealth -= 50;
        if (BaseEnemy.currentHealth <= 0)
        {
          Destroy(Enemy1);
          BaseEnemy.currentHealth = 100;
        }
      }

      if (hit.rigidbody.tag == "Enemy")
      {
        BaseEnemy.currentHealth -= 50;
        if (BaseEnemy.currentHealth <= 0)
        {
          Destroy(Enemy2);
          BaseEnemy.currentHealth = 100;
        }
      }

      if (hit.rigidbody.tag == "Enemy")
      {
        BaseEnemy.currentHealth -= 50;
        if (BaseEnemy.currentHealth <= 0)
        {
          Destroy(Enemy3);
          BaseEnemy.currentHealth = 100;
        }
      }

      if (hit.rigidbody.tag == "Enemy")
      {
        BaseEnemy.currentHealth -= 50;
        if (BaseEnemy.currentHealth <= 0)
        {
          Destroy(Enemy4);
          BaseEnemy.currentHealth = 100;
        }
      }

      //knockback from bullet
      if (hit.rigidbody != null)
      {
        hit.rigidbody.AddForce(-hit.normal * impactForce);
      }
    }
  }
}
