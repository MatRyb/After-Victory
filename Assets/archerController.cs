using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class archerController : characterScript
{
    public GameObject weapoN;
    public bool canAttack = true;
    public QuadDrawingScript quadDrawing;
    public float abilityCooldown;
    private float lastCastTime = 0.0f;
    weapon weaponScript;
    public GameObject o;

    private float maxHealth;
    public HealthBar healthBar;
    private void Start()
    {
        maxHealth = health;
        healthBar.setMaxHealth(maxHealth);
        weaponScript = weapoN.GetComponent<weapon>();
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
    public void attack()
    {
        Debug.Log("aaaaaa");
        if (Input.GetMouseButtonDown(0))
        {
                weaponScript.fire();
            lastCastTime = Time.realtimeSinceStartup;
        }
    }
}
