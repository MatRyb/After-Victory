using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellcasterEnemy : ShootingEnemy
{
    private bool canShoot = true;
    private bool shootsRay = false;
    public GameObject[] targets;
    public Material material;

    private LineRenderer lineRenderer;
    private Vector3[] Arraywithpositions = new Vector3[2];
    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = material;
        lineRenderer.widthMultiplier = 0.2f;
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        FindNearestPlayer();
        if (shootsRay)
        {
            Arraywithpositions[0] = transform.position;
            Arraywithpositions[1] = target.transform.position;
            lineRenderer.SetPositions(Arraywithpositions);
        }
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
        yield return new WaitForSeconds(attackSpeed);
        shootsRay = true;
        yield return new WaitForSeconds(2);
        shootsRay = false;
        Vector3 zero = new Vector3(0, 0, 0);
        Arraywithpositions[0] = zero;
        Arraywithpositions[1] = zero;
        lineRenderer.SetPositions(Arraywithpositions);
        canShoot = true;
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
