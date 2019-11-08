using UnityEngine;

public class Player : Actor
{
    public float moveSpeed;
    public PathfindingManager pathfinding;

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

        Cell cell = pathfinding.getCellfromWorldPos(transform.position);
        if (cell != null)
            Debug.Log(cell.x + " " + cell.y);
    }
}

