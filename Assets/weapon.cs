using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefarb;
    public float fireForce = 20f;

    public AudioSource audioSource;
    public AudioClip clip1;

    public void fire()
    {
        GameObject bullet = Instantiate(bulletPrefarb, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(Camera.main.ScreenToWorldPoint(Input.mousePosition) * fireForce, ForceMode2D.Impulse);
        audioSource.PlayOneShot(clip1, 4f);
    }
}