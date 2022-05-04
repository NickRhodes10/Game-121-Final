using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
  //Camera or "Mouse" sensitivity.
  [Header("Mouse Sensitivity:")]
  public float sensitivity = 1.0f;
  public float smoothing = 3.0f;

  public GameObject character;
  private Vector2 mouseLook;
  private Vector2 smoothV;

  void Start()
  {
    character = this.transform.parent.gameObject;
  }

  void Update()
  {
    //Vector2 from user mouse input to be smoothed.
    var mouseDir = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
    var mouseSmooth = new Vector2(sensitivity * smoothing, sensitivity * smoothing);

    //Assign movement direction based on created vectors. Smooth movement.
    mouseDir = Vector2.Scale(mouseDir, mouseSmooth);
    smoothV.x = Mathf.Lerp(smoothV.x, mouseDir.x, 1f / smoothing);
    smoothV.y = Mathf.Lerp(smoothV.y, mouseDir.y, 1f / smoothing);

    //Assign another vector to smoothed vector and limit the vertical movement. 
    mouseLook += smoothV;
    mouseLook.y = Mathf.Clamp(mouseLook.y, -75f, 90f);

    //Move mouse with smoothing.
    transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
    character.transform.localRotation = Quaternion.AngleAxis(-mouseLook.x, Vector3.down);
  }
}
