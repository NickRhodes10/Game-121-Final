using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Register : MonoBehaviour
{
  public static Register instance;

  private Dictionary<Collider, BaseEnemy> _bodyParts = new Dictionary<Collider, BaseEnemy>();

  private void Awake()
  {
    if (Register.instance == null)
      Register.instance = this;
    else if (Register.instance != this)
      Destroy(this);
  }

  public void RegisterBodyPart(Collider col, BaseEnemy enemy)
  {
    if (_bodyParts.ContainsKey(col) == false)
      _bodyParts.Add(col, enemy);
  }
}
