using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    // Shared movement speed
    [SerializeField] protected float moveSpeed = 10f;

    // Shared Rigidbody2D
    protected Rigidbody2D rb;

    // Called when the object is created
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        float input = GetInput();
        rb.velocity = new Vector2(0, input * moveSpeed);
    }

    protected virtual float GetInput()
    {
        // no movement
        return 0f;
    }
}
