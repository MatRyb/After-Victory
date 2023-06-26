using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atackArea : MonoBehaviour
{
    public int damage = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyController target = collision.GetComponent<EnemyController>();
            if (target.vln)
            {
                target.hitPoints -= damage + 3;
            }
            else
            {
                target.hitPoints -= damage;
            }
            target.stun = true;
        }
    }
}
