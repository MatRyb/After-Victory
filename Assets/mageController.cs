using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mageController : characterScript
{
    public AudioSource audioSource;
    public AudioClip clip1;


    public archerController archer;
    public warriorController warrior;
    public guardianController guardian;
    public GameObject debuffArea;
    private bool canUseSpell = true;
    public QuadDrawingScript quadDrawing;
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
        if (health <= 0)
        {
            quadDrawing.CooldownProgress = 1000.0f;
            o.SetActive(true);
        }
    }
    public void Attack()
    {
        if (canUseSpell)
        {
            Debug.Log("Spell");
            StartCoroutine(Ability());
            lastCastTime = Time.realtimeSinceStartup;
        }
    }
    
    IEnumerator Ability()
    {
        Debug.Log("debuffing");
        audioSource.PlayOneShot(clip1);
        archer.health += 5;
        guardian.health += 5;
        this.health += 5;
        warrior.health += 5;

        canUseSpell = false;
        debuffArea.SetActive(true);
        yield return new WaitForSeconds(0.10f);
        debuffArea.SetActive(false);
        yield return new WaitForSeconds(6.0f);
        canUseSpell = true;
    }
}
