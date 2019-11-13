using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Arrow : MonoBehaviour
{
    public float destroyAfter;
    private float timer = 0;

    private void Update() {
        timer += Time.deltaTime;
        if (timer > destroyAfter)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
}
