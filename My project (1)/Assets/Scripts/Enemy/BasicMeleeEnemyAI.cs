using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMeleeEnemyAI : MonoBehaviour
{

    private static class BasicMeleeAnimations
    {
        public static readonly string IDLE = "BasicMeleeIdle";
        public static readonly string WALK = "BasicMeleeWalk";
        public static readonly string START_ATK = "BasicMeleeStartAtk";
        public static readonly string RESOLVE_ATK = "BasicMeleeResolveAtk";
        
    }

    public float visionRadius =3f;

    public float attackRadius = 0.5f;
    public float windUpTime = 1f;

    public float moveSpeed = 1f;

    private string currentAnimation;

    private EnemyAttackCaster caster;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private float attackTime;
    private bool isAttacking;

    private float attackCooldown;

    private Vector2 targetX;

    private FrameByFrameAnimationController ac;

    private LayerMask playerLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerLayerMask = LayerMask.GetMask("Player");
        sprite = GetComponent<SpriteRenderer>();
        ac = GetComponent<FrameByFrameAnimationController>();
        caster = GetComponent<EnemyAttackCaster>();
        currentAnimation = BasicMeleeAnimations.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate() 
    {

        checkforPlayer();

        ac.ChangeAnimation(currentAnimation);

    }

    private void checkforPlayer()
    {
        //Collider2D[] player = Physics2D.OverlapCircleAll(transform.position, attackRadius, playerLayerMask);

        if(CheckResolveAttack())
        {
            caster.CastAttack(targetX);
            return;
        }

        Collider2D[] playerInRange = Physics2D.OverlapCircleAll(transform.position, visionRadius, playerLayerMask);

        Color rayColor;
        if(playerInRange.Length > 0)
        {

            if(Vector2.Distance(playerInRange[0].transform.position, transform.position) > attackRadius)
            {
                rayColor = Color.green;
            }
            else
            {
                rayColor = Color.red;
            }

        }
        else
            rayColor = Color.red;

        Debug.DrawRay(transform.position, new Vector2(attackRadius, 0), rayColor);

        Debug.DrawRay(transform.position, new Vector2(-attackRadius, 0), rayColor);


        if(playerInRange.Length > 0 && Time.time > attackCooldown && !isAttacking)
        {
            if(Vector2.Distance(playerInRange[0].transform.position, transform.position) > attackRadius)
            {
                MoveToTarget(playerInRange[0].transform.position);
            }
            else
            {
                Attack(playerInRange[0].transform.position);
                targetX = new Vector2(playerInRange[0].transform.position.x, 0);
            }
        }

        if(playerInRange.Length == 0 && !isAttacking)
        {
            currentAnimation = BasicMeleeAnimations.IDLE;
        }
    }

    private bool CheckResolveAttack()
    {
        if(isAttacking && Time.time > attackTime)
        {
            isAttacking = false;
            currentAnimation = BasicMeleeAnimations.RESOLVE_ATK;
            attackCooldown = Time.time + 1f;
            return true;
        }
        return false;
    }

    private void Attack(Vector2 targetPosition)
    {
        currentAnimation = BasicMeleeAnimations.START_ATK;
        attackTime = Time.time + windUpTime;
        isAttacking = true;
        if(targetPosition.x - transform.position.x > 0)
            sprite.flipX = true;
        else
            sprite.flipX = false;
    }

    private void MoveToTarget(Vector2 targetPosition)
    {
        //rb.velocity = (targetPosition - new Vector2(transform.position.x, transform.position.y)).normalized * moveSpeed;
        var newV = new Vector2(targetPosition.x - transform.position.x, 0).normalized * moveSpeed;
        if(rb.velocity.x > moveSpeed)
            newV = new Vector2(moveSpeed, 0);
        else if(rb.velocity.x < -moveSpeed)
            newV = new Vector2(-moveSpeed, 0);
        
        
        rb.velocity = new Vector2(newV.x, rb.velocity.y);
        
        currentAnimation = BasicMeleeAnimations.WALK;

        if(targetPosition.x - transform.position.x > 0)
            sprite.flipX = true;
        else
            sprite.flipX = false;
    }
}
