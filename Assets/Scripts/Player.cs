using UnityEngine;

public class Player : Actor
{
    public float moveSpeed;
    public PathfindingManager pathfinding;
    public TurretSpot currentTurret = null;

    private void Update()
    {
        Vector2 moveDir;
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");
        float moveMag = Mathf.Clamp(moveDir.magnitude, 0f, 1f);
        moveDir = moveDir.normalized * moveMag;
        moveVec = moveDir * moveSpeed;

        if (moveVec.x > 0) sr.flipX = false;
        if (moveVec.x < 0) sr.flipX = true;
        animator.SetFloat("Speed", moveDir.magnitude);

        if (currentTurret != null)
        {
            if (Input.GetButtonDown("Action"))
            {
                currentTurret.UpgradeTurret();
            }
        }
    }
}

