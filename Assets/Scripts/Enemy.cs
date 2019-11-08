using UnityEngine;

public class Enemy : Actor
{
    private GameObject target;

    public float moveSpeed;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        moveVec = target.transform.position - transform.position;
        moveVec.Normalize();
        moveVec *= moveSpeed;

        if (moveVec.x > 0) sr.flipX = true;
        if (moveVec.x < 0) sr.flipX = false;
    }
}
