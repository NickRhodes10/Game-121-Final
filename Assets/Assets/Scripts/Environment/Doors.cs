using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
  AudioSource DoorAudioSource;
  Animator DoorAnimations;

  public void Start()
  {
    DoorAnimations = gameObject.GetComponent<Animator>();
    DoorAudioSource = GetComponent<AudioSource>();
  }

  public void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player" || other.tag == "Enemy")
      OpenDoor();
  }

  public void OpenDoor()
  {
    DoorAnimations.SetBool("characterNear", true);
    DoorAudioSource.Play();
    StartCoroutine(CloseDoor());
  }

  IEnumerator CloseDoor()
  {
    yield return new WaitForSeconds(3f);
    GetComponent<Animator>().SetBool("characterNear", false);
  }
}
