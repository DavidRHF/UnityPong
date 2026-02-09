using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PaddleController : MonoBehaviour, ICollidable
{
    // Shared movement speed
    [SerializeField] protected float moveSpeed = 10f;
    private Vector2 direction;
    protected Rigidbody2D rb;

    // Called when the object is created
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = new Vector2(1f, 1f).normalized;
    }

    protected virtual void FixedUpdate()
    {
        float input = GetInput();
        rb.velocity = new Vector2(0, input * moveSpeed);
    }

    
    protected abstract float GetInput();

    // ICollidable
    public virtual void OnHit(Collision2D collision)
    {
        Debug.Log($"{gameObject.name} was hit by the ball");
    }
}
