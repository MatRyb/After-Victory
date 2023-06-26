using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyController
{

    bool canAttack = true;
    public bool isPushedBack = false;
    private bool pushingBackInumer = true;
    public GameObject[] targets;
    void Start()
    {
        targets[0] = GameObject.FindGameObjectWithTag("Player1");
        targets[1] = GameObject.FindGameObjectWithTag("Player2");
        targets[2] = GameObject.FindGameObjectWithTag("Player3");
        targets[3] = GameObject.FindGameObjectWithTag("Player4");
    }

    void Update()
    {
        FindNearestPlayer();
        if (stun)
        {
            StartCoroutine(StunTime());
        }
        if(hitPoints <= 0)
        {
            Destroy(gameObject);
        }

        
            Vector2 pos = transform.position;
            Vector2 targetPos = target.transform.position;
        if (!isPushedBack)
        {
            float distance = Vector2.Distance(pos, targetPos);

            if (attackRange >= distance)
            {
                if (canAttack)
                {
                    Attack();
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(pos, targetPos, moveSpeed * Time.deltaTime);
            }
        }
        if (isPushedBack)
        {

            transform.position = Vector2.MoveTowards(pos, (pos-targetPos).normalized + pos, moveSpeed * Time.deltaTime);
            if (pushingBackInumer)
            {
                StartCoroutine(PushingBack());
            }
        }
    }

    IEnumerator PushingBack()
    {
        pushingBackInumer = false;
        yield return new WaitForSeconds(2.0f);
        pushingBackInumer = true;
        isPushedBack = false;
    }

    void Attack()
    {
        if (canAttack)
        {
            StartCoroutine(MeleeAttack());
        }
    }

    IEnumerator MeleeAttack()
    {
        canAttack = false;
        target.GetComponent<characterScript>().health -= attackDamage;
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }

    IEnumerator StunTime()
    {
        Debug.Log("stun");
        stun = false;
        float storeMoveSpeed = moveSpeed;
        float storeDamage = attackDamage;
        moveSpeed *= 0;
        attackDamage *= 0f; 
        if(vln)
        {
            yield return new WaitForSeconds(3.0f);
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
        }
        moveSpeed = storeMoveSpeed;
        attackDamage = storeDamage;
    }

    void FindNearestPlayer()
    {

        Vector3 pos = transform.position;
        float dist = float.PositiveInfinity;
        GameObject targ = null;
        foreach (var obj in targets)
        {
            var d = (pos - obj.transform.position).sqrMagnitude;
            if (d < dist)
            {
                targ = obj;
                dist = d;
            }
        }
        target = targ;
    }
}
