using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
  [Header("Rocket Launcher Settings")]
  [SerializeField] public int damage = 100;
  [SerializeField] public float range = 30f;
  [SerializeField] public float impactForce;
  [SerializeField] public float nextTimeToFire = 3.5f;
  [SerializeField] public int startingAmmo = 5;

  public float shotDelay = 0.5f;

  [Header("Enter GameObjects")]
  public GameObject muzzlePrefab;
  public GameObject muzzlePosition;

  public GameObject projectilePrefab;
  public GameObject projectileToDisableOnFire;

  [Header("Audio Settings")]
  public AudioClip GunShotClip;
  public AudioClip ReloadClip;
  public AudioSource source;
  public AudioSource reloadSource;
  public Vector2 audioPitch = new Vector2(.9f, 1.1f);

  [Header("Timing")]
  private float timeLastFired;

  private int ammo;

  private void Start()
  {
    if (source != null) source.clip = GunShotClip;
    nextTimeToFire = 0;
    ammo = startingAmmo;
  }

  private void Update()
  {
    if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && ammo > 0)
    {
      nextTimeToFire = 4;
      FireLauncher();
    }
    nextTimeToFire -= Time.deltaTime;
  }

  public void FireLauncher()
  {
    timeLastFired = Time.time;
    nextTimeToFire = 4;
    ammo--;

    var flash = Instantiate(projectilePrefab, muzzlePosition.transform.position, muzzlePosition.transform.rotation, transform);

    if (projectilePrefab != null)
    {
      GameObject newProjectile = Instantiate(projectilePrefab, muzzlePosition.transform.position, muzzlePosition.transform.rotation, transform);
      Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
      rb.AddForce(transform.forward * 500, ForceMode.VelocityChange);
    }

    if (projectileToDisableOnFire != null)
    {
      projectileToDisableOnFire.SetActive(false);
      Invoke("ReEnableDisabledProjectile", 3);
    }

    if (source != null)
    {
      if (source.transform.IsChildOf(transform))
      {
        source.Play();
      }
      else
      {
        AudioSource newAS = Instantiate(source);
        if ((newAS = Instantiate(source)) != null && newAS.outputAudioMixerGroup != null && newAS.outputAudioMixerGroup.audioMixer != null)
        {
          newAS.outputAudioMixerGroup.audioMixer.SetFloat("Pitch", Random.Range(audioPitch.x, audioPitch.y));
          newAS.pitch = Random.Range(audioPitch.x, audioPitch.y);

          newAS.PlayOneShot(GunShotClip);

          Destroy(newAS.gameObject, 3);
        }
      }
    }
  }
}
