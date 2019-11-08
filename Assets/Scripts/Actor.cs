using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Actor : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;

    [SerializeField]
    protected Vector2 moveVec;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        rb.velocity = moveVec;
    }
}
