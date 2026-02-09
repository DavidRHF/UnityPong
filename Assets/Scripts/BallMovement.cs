using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour, ICollidable
{
    // Private fields (encapsulated data)
    [SerializeField] private float speed = 3f;
    private Vector2 direction;
    private Rigidbody2D rb;

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
        //collision handling
        ICollidable collidable =
            collision.gameObject.GetComponent<ICollidable>();

        if (collidable != null)
        {
            collidable.OnHit(collision);
        }

        //reacts to the collision
        OnHit(collision);
    }

    public void OnHit(Collision2D collision)
    {
        Vector2 normal = collision.contacts[0].normal;
        Direction = Vector2.Reflect(direction, normal);
    }
}
