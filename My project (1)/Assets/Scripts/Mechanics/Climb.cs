using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{    
    public float climbSpeed = 5f;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private bool isClimbing = false;
    private float climbVelocity;
    private float gravityStore;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        climb();
    }

    private void climb()
    {
        // Check if the player is touching a wall
        if (IsTouchingWall() && Input.GetKeyDown(KeyCode.Space))
        {
            isClimbing = true;
            gravityStore = rb.gravityScale;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }

        if (isClimbing)
        {
            // Get the direction to climb up or down
            climbVelocity = Input.GetAxisRaw("Vertical") * climbSpeed;
            rb.velocity = new Vector2(rb.velocity.x, climbVelocity);

            // If the player stops climbing or reaches the top/bottom of the wall, turn off climbing mode
            if (!IsTouchingWall() || Input.GetKeyUp(KeyCode.Space))
            {
                isClimbing = false;
                rb.gravityScale = gravityStore;
                rb.velocity = new Vector2(Input.GetAxis("Horizontal") * climbSpeed, 0) + Vector2.up;
            }
        }
    }

    private bool IsTouchingWall()
    {
        // Check if the player's collider is touching a wall layer
        Vector2 bottomLeft = new Vector2(boxCollider.bounds.center.x - boxCollider.bounds.extents.x, boxCollider.bounds.min.y - 0.1f);
        Vector2 topRight = new Vector2(boxCollider.bounds.center.x + boxCollider.bounds.extents.x, boxCollider.bounds.min.y + 0.1f);
        return Physics2D.OverlapArea(bottomLeft, topRight, LayerMask.GetMask("Wall"));
    }

}
