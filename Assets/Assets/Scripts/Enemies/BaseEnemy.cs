using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
  public int maxHealth = 100;
  public static int currentHealth;
  protected NavMeshAgent agent;
  protected Animator anim;

  public int CurrentHealth { get { return currentHealth; } set { value = currentHealth; } }

  private void Start()
  {
    currentHealth = maxHealth;
    agent = GetComponent<NavMeshAgent>();
    anim = GetComponent<Animator>();
    RegisterSelf();
    ToggleRigidbodyKinematics(true);
  }

  public void TakeDamage()
  {
    currentHealth -= 50;

    if(currentHealth < 0)
    {
      Debug.Log(transform.name + "is dead.");
      agent.isStopped = true;
      anim.enabled = false;
      ToggleRigidbodyKinematics(false);
      Destroy(gameObject, 5);
      return;

    }
  }

  private void RegisterSelf()
  {
    Collider[] cols = GetComponentsInChildren<Collider>();

    foreach(Collider col in cols)
    {
      Register.instance.RegisterBodyPart(col, this);
    }
  }

  private void ToggleRigidbodyKinematics(bool turnOn)
  {
    Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();
    foreach(Rigidbody rb in rbs)
    {
      rb.isKinematic = turnOn;
      rb.GetComponent<Collider>().enabled = !turnOn;
    }
  }
}
