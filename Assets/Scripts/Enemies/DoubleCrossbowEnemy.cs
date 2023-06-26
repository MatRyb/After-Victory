using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleCrossbowEnemy : ShootingEnemy
{

    private bool canShoot = true;
    public GameObject bullet;
    public Transform bulletStorage;
    float progress = 0.0f;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Character");
        moveSpeed /= 4.0f;
        hitPoints = 2.0f;

    }

    void Update()
    {
        if (hitPoints <= 0)
        {
            Destroy(gameObject);
        }
        progress += moveSpeed * Time.deltaTime ;
        Vector2 pos = transform.position;
        Vector2 targetPos = target.transform.position;
        float rad = progress * Mathf.PI * 2;
        float distance = Vector2.Distance(pos, targetPos);
        Vector2 circle = new Vector2(Mathf.Cos(rad), -Mathf.Sin(rad));
        Vector2 targetV = (circle * attackRange + (targetPos - pos));
        transform.position += new Vector3(targetV.x, targetV.y, 0.0f) * 0.2f;
        if(distance <= attackRange)
        {
            if (canShoot)
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        moveSpeed *= 4.0f;
        canShoot = false;
        for (int i = 0; i < 4; i++)
        {
            spawnBullets();
            Debug.Log("I attack!");
            yield return new WaitForSeconds(1.0f);
        }
        moveSpeed /= 4.0f;
        yield return new WaitForSeconds(attackSpeed);
        
        Debug.Log("Im here!");
        canShoot = true;
    }

    void spawnBullets()
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity, bulletStorage);
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y) * bulletSpeed;
        newBullet.GetComponent<EnemyBullet>().damage = attackDamage;
    }
}
