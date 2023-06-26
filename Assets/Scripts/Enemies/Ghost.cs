using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : ShootingEnemy
{
    private bool canShoot = true;
    public GameObject bullet;
    public Transform bulletStorage;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector2 pos = transform.position;
        Vector2 targetPos = target.transform.position;
        float distance = Vector2.Distance(pos, targetPos);
        if (attackRange >= distance)
        {
            if (canShoot)
                Attack();
        }
        else
        {
            transform.position = Vector2.MoveTowards(pos, targetPos, moveSpeed * Time.deltaTime);
            Debug.Log("I go!");
        }
    }

    void Attack()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        spawnBullets();
        yield return new WaitForSeconds(attackSpeed);
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
