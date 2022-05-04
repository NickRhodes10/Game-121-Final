using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  //Basic movement settings to be changed in inspector.
  [Header("Movement Settings:")]
  [SerializeField] private float moveSpeed;
  [SerializeField] private float walkSpeed;
  [SerializeField] private float runSpeed;
  [SerializeField] private float health = 100;

  //Checks in place to confirm player can move

  [Header("Jump Height:")]
  [SerializeField] private float jumpHeight = 1.0f;

  private float gravity = -9.81f; //acceleration
  private bool groundedPlayer;
  private Vector3 velocity;

  public BigRookGames.Weapons.GunfireController shoot;

  private CharacterController _controller;

  private void Start()
  {
    _controller = GetComponent<CharacterController>();
  }

  private void Update()
  {
    Movement();

    //Jump if user presses "space"
    if (Input.GetButton("Jump") && groundedPlayer)
      Jump();

    velocity.y += gravity * Time.deltaTime;
    _controller.Move(velocity * Time.deltaTime);
  }

  private void Movement()
  {
    groundedPlayer = _controller.isGrounded;
    if (groundedPlayer && velocity.y < 0)
      velocity.y = 0f;

    //Create a vector3 for player movement based on user input with WASD.
    Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    move = transform.TransformDirection(move);

    //Move player using the Vector3 created & smooth movement speed.
    _controller.Move(move * Time.deltaTime * moveSpeed);

    //If player is moving and not sprinting, walk. Else run.
    if (move != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
      Walk();
    else if (move != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
      Run();
  }

  private void Walk()
  {
    moveSpeed = walkSpeed;
  }

  private void Run()
  {
    moveSpeed = runSpeed;
  }

  private void Jump()
  {
    //Add force to y axis to perform jump.
    velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
  }
}
