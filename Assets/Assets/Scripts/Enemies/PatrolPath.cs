using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
  [SerializeField] Color gizmoColor = Color.red;

  private Transform[] waypointsArray;

  public Transform[] WaypointsArray { get { return waypointsArray; } set { value = waypointsArray; } }

  private void Awake()
  {
    waypointsArray = new Transform[transform.childCount];
    for (int i = 0; i < transform.childCount; i++)
    {
      waypointsArray[i] = transform.GetChild(i).GetComponent<Transform>();
    }
  }

  private void OnDrawGizmos()
  {
    if (waypointsArray == null)
      return;
    for(int i = 0; i < waypointsArray.Length; i++)
    {
      Gizmos.color = gizmoColor;
      Gizmos.DrawSphere(waypointsArray[i].position, 5f);
      if(i < waypointsArray.Length - 1)
      {
        Gizmos.DrawLine(waypointsArray[i].position, waypointsArray[i + 1].position);
        Gizmos.color = gizmoColor;
      }
      else
      {
        Gizmos.DrawLine(waypointsArray[i].position, waypointsArray[0].position);
        Gizmos.color = gizmoColor;
      }
    }
  }
}
