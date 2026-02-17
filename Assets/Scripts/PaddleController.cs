using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class PaddleController : NetworkBehaviour, ICollidable
{
    // Shared movement speed
    [SerializeField] protected float moveSpeed = 10f;
    private Vector2 direction;
    protected Rigidbody2D rb;

    private NetworkVariable<float> syncedYPosition =
        new NetworkVariable<float>(0f);
    // Called when the object is created
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = new Vector2(1f, 1f).normalized;
    }

    protected virtual void FixedUpdate()
    {
        if (IsOwner)
        {
            float input = GetInput();
            rb.velocity = new Vector2(0, input * moveSpeed);

            syncedYPosition.Value = rb.position.y;
        }
        else
        {
            rb.position = new Vector2(
                rb.position.x,
                syncedYPosition.Value
            );
        }
    }


    protected abstract float GetInput();

    // ICollidable
    public virtual void OnHit(Collision2D collision)
    {
        Debug.Log($"{gameObject.name} was hit by the ball");
    }
}
