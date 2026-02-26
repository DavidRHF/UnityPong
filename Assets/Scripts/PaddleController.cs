using Unity.Netcode;
using UnityEngine;

public class PaddleController : NetworkBehaviour, ICollidable
{
    [SerializeField] private float moveSpeed = 10f;

    private Rigidbody2D rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.freezeRotation = true;
    }

    protected virtual void FixedUpdate()
    {
        if (!IsOwner) return;

        float input = 0f;

        // Use SAME keys for whoever owns this paddle
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            input = 1f;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            input = -1f;

        rb.velocity = new Vector2(0, input * moveSpeed);
    }

    private float GetHostInput()
    {
        float move = 0f;
        if (Input.GetKey(KeyCode.W))
            move = 1f;
        else if (Input.GetKey(KeyCode.S))
            move = -1f;

        return move;
    }

    private float GetClientInput()
    {
        float move = 0f;
        if (Input.GetKey(KeyCode.UpArrow))
            move = 1f;
        else if (Input.GetKey(KeyCode.DownArrow))
            move = -1f;

        return move;
    }

    public virtual void OnHit(Collision2D collision)
    {
    // ICollidable
    }
}