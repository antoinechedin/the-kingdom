using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public float moveSpeed;

    private Vector2 moveVec;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

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
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVec * Time.fixedDeltaTime);
    }
}
