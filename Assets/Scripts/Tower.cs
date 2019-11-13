using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float viewRadius;
    public float firingRate;
    public float projectileSpeed;
    public LayerMask blockingLayer;

    public GameObject arrowPrefab;

    private GameObject target;
    private float reloading = 0;

    private void Update()
    {
        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
            RaycastHit2D hit = Physics2D.CircleCast(
                transform.position,
                0.15f,
                target.transform.position - transform.position,
                distanceToTarget - 0.15f,
                blockingLayer
            );
            if (hit.collider != null || distanceToTarget > viewRadius)
                target = null;
        }
        else
        {
            float distance = viewRadius;
            foreach (GameObject tempTarget in GameObject.FindGameObjectsWithTag("Player"))
            {
                float tempDist = Vector3.Distance(tempTarget.transform.position, transform.position);
                RaycastHit2D hit = Physics2D.CircleCast(
                    transform.position,
                    0.15f,
                    tempTarget.transform.position - transform.position,
                    tempDist - 0.15f,
                    blockingLayer
                );
                if (hit.collider == null && tempDist < distance)
                {
                    target = tempTarget;
                    distance = tempDist;
                }
            }
        }

        if (target != null)
        {
            if (reloading == 0)
            {
                Vector3 shootDir = (target.transform.position - transform.position).normalized;
                float angle = Vector3.SignedAngle(Vector3.up, shootDir, Vector3.forward);
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.Euler(0, 0, angle), transform);

                arrow.GetComponent<Rigidbody2D>().velocity = shootDir * projectileSpeed;
                reloading = 1f / firingRate;
            }
        }

        if (reloading > 0)
        {
            reloading -= Time.deltaTime;
            reloading = reloading < 0 ? 0 : reloading;
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
        if (target != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, target.transform.position);
        }
    }
}
