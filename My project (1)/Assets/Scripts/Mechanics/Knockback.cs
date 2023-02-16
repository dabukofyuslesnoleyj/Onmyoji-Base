using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float defaultKnockbackForce = 10f;
    public float knockbackDuration = 0.5f;
    private float currentKnockbackForce;

    private Rigidbody2D rigidBody;
    private float knockbackStartTime;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        /*if (Time.time < knockbackStartTime + knockbackDuration)
        {
            rigidBody.velocity = Vector2.zero;
            Vector2 knockbackDirection = transform.position - (Vector3)rigidBody.position;
            knockbackDirection = -knockbackDirection.normalized;
            //rigidBody.gravityScale = 0;
            rigidBody.AddForce(knockbackDirection * currentKnockbackForce, ForceMode2D.Impulse);
        }*/
    }

    public void ApplyDefaultKnockback(Vector2 knockbackDirection)
    {
        currentKnockbackForce = defaultKnockbackForce;
        knockbackStartTime = Time.time;
        rigidBody.velocity = Vector2.zero;
        knockbackDirection = -knockbackDirection.normalized;
        rigidBody.AddForce(knockbackDirection * currentKnockbackForce, ForceMode2D.Impulse);
    }

    public void ApplyKnockback(Vector2 knockbackDirection, float knockbackForce)
    {
        currentKnockbackForce = knockbackForce;
        knockbackStartTime = Time.time;
        rigidBody.velocity = Vector2.zero;
        knockbackDirection = -knockbackDirection.normalized;
        rigidBody.AddForce(knockbackDirection * currentKnockbackForce, ForceMode2D.Impulse);
    }
}
