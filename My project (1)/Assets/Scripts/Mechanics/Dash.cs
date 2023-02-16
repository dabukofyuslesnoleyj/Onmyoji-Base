using UnityEngine;
using System.Collections.Generic;

public class Dash : MonoBehaviour
{
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    private float dashTime;
    private bool isDashing;
    private Rigidbody2D rb;
    private Vector2 dashDirection;
    private BoxCollider2D boxCollider;

    private IDashListener listeners;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            rb.velocity = dashDirection * dashSpeed;

            if (Time.time >= dashTime)
            {
                isDashing = false;
                boxCollider.enabled = true;
                rb.velocity = Vector2.zero;
                onFinishDash();
            }
            else
            {
                duringDash();
            }
        }
    }

    public void CallDash(Vector2 targetPosition)
    {

        Vector2 objectPosition = transform.position;
        dashDirection = (targetPosition - objectPosition).normalized;
        Debug.Log("player position: " + objectPosition);
        Debug.Log("target Position" + targetPosition);
        dashTime = Time.time + dashDuration;
        boxCollider.enabled = false;
        isDashing = true;
        onStartDash();
    }

    public bool GetIsDashing()
    {
        return isDashing;
    }

    private void onFinishDash()
    {
        listeners.OnFinishDash();
    }
    private void onStartDash()
    {
        listeners.OnStartDash();
    }
    private void duringDash()
    {
        listeners.DuringDash();
    }

    public void AddListener(IDashListener d)
    {
        listeners = d;
    }
}
