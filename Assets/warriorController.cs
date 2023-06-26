using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class warriorController : characterScript
{
    public AudioSource audioSource;
    public AudioClip clip1;

    private bool canAttack = true;
    public GameObject slash;
    public SlashController slashController;
    public QuadDrawingScript quadDrawing;
    public float abilityCooldown;
    public GameObject o;


    private float lastCastTime = 0.0f;

    private float maxHealth;
    public HealthBar healthBar;

    private void Start()
    {
        maxHealth = health;
        healthBar.setMaxHealth(maxHealth);
        
    }

    private void Update()
    {
        healthBar.setHealth(health);
        quadDrawing.CooldownProgress = (Time.realtimeSinceStartup - lastCastTime) / abilityCooldown;
        if (health <= 0)
        {
            quadDrawing.CooldownProgress = 1000.0f;
            o.SetActive(true);
        }
    }
    public void Attack()
   {
        if (canAttack)
        {
            StartCoroutine(SlashAbility());
            lastCastTime = Time.realtimeSinceStartup;
        }
   }
    IEnumerator SlashAbility()
    {
        canAttack = false;
        slash.SetActive(true);
        slashController.Slash();
        audioSource.PlayOneShot(clip1);
        yield return new WaitForSeconds(1f);
        slash.SetActive(false);
        yield return new WaitForSeconds(abilityCooldown-1);
        canAttack = true;
    }
}
