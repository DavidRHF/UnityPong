using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    // variable declaring
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        // physics components
        rb = GetComponent<Rigidbody2D>();

        // begins movement immediately
        rb.velocity = new Vector2(3f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 normal = collision.contacts[0].normal;

        // Reflect velocity based on surface angle
        rb.velocity = Vector2.Reflect(rb.velocity, normal);

    }
    */
}
