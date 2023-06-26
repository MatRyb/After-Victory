using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guardianController : characterScript
{
    public AudioSource audioSource;
    public AudioClip clip1;

    public GameObject shieldBash;
    public QuadDrawingScript quadDrawing;
    bool canUseAbility = true;
    public float abilityCooldown;
    private float lastCastTime = 0.0f;

    private float maxHealth;
    public HealthBar healthBar;
    public GameObject o;

    private void Start()
    {
        maxHealth = health;
        healthBar.setMaxHealth(maxHealth);

    }
    private void Update()
    {
        healthBar.setHealth(health);
        quadDrawing.CooldownProgress = (Time.realtimeSinceStartup - lastCastTime) / abilityCooldown;
        if(health <= 0)
        {
            quadDrawing.CooldownProgress = 1000.0f;
            o.SetActive(true);
        }
        
    }

    public void Attack()
    {
        if (canUseAbility)
        {
            Debug.Log("I use ability!");
            StartCoroutine(ShieldAbility());
            lastCastTime = Time.realtimeSinceStartup;
        }
    }

    IEnumerator ShieldAbility()
    {
        Debug.Log("I use ability!");
        canUseAbility = false;
        //shieldBash.SetActive(true);
        shieldBash.GetComponent<ShieldBash>().detectionWorking = true;
        audioSource.PlayOneShot(clip1);
        yield return new WaitForSeconds(0.3f);
        //shieldBash.SetActive(false);
        shieldBash.GetComponent<ShieldBash>().detectionWorking = false;
        yield return new WaitForSeconds(abilityCooldown-0.3f);
        canUseAbility = true;
    }
}
