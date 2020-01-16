using System.Collections.Generic;
using UnityEngine;

public class Monster : Actor
{
    public int maxLife;
    public PathfindingManager pm;
    public Vector2 target;

    public bool followPlayer;
    private List<Cell> path;

    public float moveSpeed;

    private float life;

    private Transform lifeBar;

    protected override void Awake()
    {
        base.Awake();
        life = maxLife;
        lifeBar = transform.GetChild(1);
    }

    private void Start()
    {
        if (followPlayer) target = GameObject.FindGameObjectWithTag("Player").transform.position;
        path = pm.FindPath(transform.position, target);
    }

    public void TakeDamage(float damage)
    {
        life -= damage;
        lifeBar.localScale = new Vector3(life * 0.78f / maxLife, lifeBar.localScale.y, lifeBar.localScale.z);
        //lifeBar.transform.localPosition = new Vector3(-life * 0.78f / (2 * maxLife), 0, 0);
        if (life <= 0) Destroy(gameObject);
    }

    private void Update()
    {
        if (followPlayer) target = GameObject.FindGameObjectWithTag("Player").transform.position;

        moveVec = Vector2.zero;

        if (path != null)
        {
            Vector3 nextTarget = transform.position;

            if (path.Count > 0)
            {
                nextTarget = path[0].worldPos;
                moveVec = nextTarget - transform.position;
                float distance = moveVec.magnitude;
                moveVec.Normalize();
                moveVec *= moveSpeed;

                if (distance < 0.2f)
                {
                    path.RemoveAt(0);
                    if (followPlayer) path = pm.FindPath(transform.position, target);
                }
            }
            if (path.Count == 0 && followPlayer)
            {
                path = pm.FindPath(transform.position, target);
            }

            if (moveVec.x > 0) sr.flipX = false;
            if (moveVec.x < 0) sr.flipX = true;
            /*if (transform.position.y < player.transform.position.y) sr.sortingOrder = 1;
            else sr.sortingOrder = -1;*/
        }
        animator.SetFloat("Speed", moveVec.magnitude / moveSpeed);
    }
}
