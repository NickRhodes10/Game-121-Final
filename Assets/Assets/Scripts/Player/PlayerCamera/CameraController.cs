using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public float speed = 10.0f;

  private float translation;
  private float cameraDrag;

  private void Start()
  {
    Cursor.lockState = CursorLockMode.Locked;
  }

  private void Update()
  {
    translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
    cameraDrag = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
    transform.Translate(cameraDrag, 0, translation);

    if (Input.GetKeyDown("escape"))
      Cursor.lockState = CursorLockMode.None;
  }
}
