using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    // Private fields (encapsulated data)
    [SerializeField] private float speed = 3f;
    private Vector2 direction;
    private Rigidbody2D rb;

    // Speed
    public float Speed
    {
        get { return speed; }
        set
        {
            if (value < 0)
            {
                speed = 0f;
            }
            else
            {
                speed = value;
            }
        }
    }

    // Direction
    public Vector2 Direction
    {
        get { return direction; }
        set { direction = value.normalized; }
    }

    // Start is called before the first frame update
    void Start()
    {
        // physics components
        rb = GetComponent<Rigidbody2D>();

        // begins movement immediately
        Direction = new Vector2(1f, 1f);
    }

    void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if a paddle
        if (collision.gameObject.CompareTag("Paddle"))
        {
            // Reverse horizontal direction
            direction.x = -direction.x;

            direction = direction.normalized;
        }
        // for walls
        else
        {
            Vector2 normal = collision.contacts[0].normal;

            Direction = Vector2.Reflect(direction, normal);
        }
    }

}
