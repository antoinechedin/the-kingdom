using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    public PathfindingManager pm;
    private GameObject target;
    private List<Cell> path;

    public float moveSpeed;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        path = pm.FindPath(transform.position, target.transform.position);
    }

    private void Update()
    {
        moveVec = Vector2.zero;
        Vector3 nextTarget = transform.position;

        if (path.Count > 0)
        {
            nextTarget = path[0].worldPos;
            moveVec = nextTarget - transform.position;
            float distance = moveVec.magnitude;
            moveVec.Normalize();
            moveVec *= moveSpeed;

            if (distance < 0.2f)
                path = pm.FindPath(transform.position, target.transform.position);
        }
        else
            path = pm.FindPath(transform.position, target.transform.position);
        // if ((path[0].worldPos - transform.position).magnitude < 0.3f)
        // {
        //     Debug.Log("Catch");
        //     path.RemoveAt(0);
        //     if (path.Count == 0)
        //     {
        //         Debug.Log("Recalculate path");
        //         //
        //         Debug.Log("Path recalculated");
        //     }
        // }

        if (moveVec.x > 0) sr.flipX = false;
        if (moveVec.x < 0) sr.flipX = true;
        animator.SetFloat("Speed", moveVec.magnitude);
    }
}
