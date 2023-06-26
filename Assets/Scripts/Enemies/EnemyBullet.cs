using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float dieTime;
    public float damage;
    //public Transform player;


    void Start()
    {
        Destroy(gameObject, dieTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("col call");
        if (collision.gameObject.CompareTag("shield") || collision.gameObject.CompareTag("archerBullet"))
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2") || collision.gameObject.CompareTag("Player3") || collision.gameObject.CompareTag("Player4"))
        {
            Debug.Log("Collide");
            DealDamage(collision);
            Destroy(gameObject);
        }
    }

    void DealDamage(Collision2D collision)
    {
        collision.gameObject.GetComponent<characterScript>().health -= damage;
    }
}
