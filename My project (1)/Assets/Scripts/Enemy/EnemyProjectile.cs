using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float TimeToLive = 0.5f;

    public float damage = 1f;

    public float knockbackForce = 10f;

    public float scanRadius = 0.2f;
    private float livedTime;
    private LayerMask playerMask;
    // Start is called before the first frame update
    void Start()
    {
        livedTime = TimeToLive + Time.time;
        playerMask = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkForPlayer();
        if(Time.time > livedTime)
        {
            Destroy(gameObject);
        }   
    }

    private void checkForPlayer()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(transform.position, scanRadius, playerMask);
        if(player.Length > 0)
        {
            GameObject p = player[0].gameObject;
            Transform pt = p.GetComponent<Transform>();
            PlayerActions pA = p.GetComponent<PlayerActions>();
            pA.PlayerDamaged(damage, transform.position, knockbackForce);
            Destroy(gameObject);
        }
    }
}
