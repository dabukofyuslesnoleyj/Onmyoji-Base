using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemyAI : MonoBehaviour
{

    public static class SmallBasicEnemyAnimations
    {
        public static string IDLE = "BasicSmallEnemyIdle";
        public static string START_ATTACK = "BasicSmallEnemyAttack";
        public static string RESOLVE_ATTACK = "BasicSmallEnemyResolveAttack";
    }

    public float visionRadius = 5.0f;
    public float moveSpeed = 3.0f;

    public float attackRadius = 0.5f;
    public float windUpTime = 0.5f;
    public float attackCooldown = 1f;

    private float coolDownTime;
    private float attackTime;

    private Rigidbody2D rb;

    private Animator animatior;
    private SpriteRenderer sprite;
    private LayerMask playerLayerMask;
    private FrameByFrameAnimationController ac;
    private string currentAnimation;
    private EnemyAttackCaster caster;

    private Vector2 targetX;

    private bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        ac = GetComponent<FrameByFrameAnimationController>();
        playerLayerMask = LayerMask.GetMask("Player");
        currentAnimation = SmallBasicEnemyAnimations.IDLE;
        isAttacking = false;
        caster = GetComponent<EnemyAttackCaster>();
        coolDownTime = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(ResolveAttack())
        {
            caster.CastAttack(targetX);
            return;
        }
        
        Collider2D[] playerInRange = Physics2D.OverlapCircleAll(transform.position, visionRadius, playerLayerMask);
        if(playerInRange.Length > 0)
        {
            Vector2 position = playerInRange[0].transform.position;

            if(position.x - transform.position.x > 0)
                sprite.flipX = true;
            else
                sprite.flipX = false;

            if(!isAttacking && Time.time > coolDownTime)
            {
                if(Vector2.Distance(position, transform.position) > attackRadius)
                    ChaseTarget(position);
                else
                {
                    if(position.x > transform.position.x)
                        targetX = new Vector2(transform.position.x + 1, 0);
                    else
                        targetX = new Vector2(transform.position.x - 1, 0);
                    Attack();
                }
            }
            if(!isAttacking && Time.time > coolDownTime)
            {
                currentAnimation = SmallBasicEnemyAnimations.IDLE;
            }
        }

        ac.ChangeAnimation(currentAnimation);
    }

    private void Attack()
    {
        currentAnimation = SmallBasicEnemyAnimations.START_ATTACK;
        isAttacking = true;
        attackTime = Time.time + windUpTime;
    }

    private bool ResolveAttack()
    {
        if(Time.time > attackTime && isAttacking)
        {
            currentAnimation = SmallBasicEnemyAnimations.RESOLVE_ATTACK;
            isAttacking = false;
            coolDownTime = Time.time + attackCooldown;
            return true;
        }
        return false;
    }

    private void ChaseTarget(Vector2 targetPosition)
    {
        var newV = new Vector2(targetPosition.x - transform.position.x, 0).normalized * moveSpeed;
        if(rb.velocity.x > moveSpeed)
            newV = new Vector2(moveSpeed, 0);
        else if(rb.velocity.x < -moveSpeed)
            newV = new Vector2(-moveSpeed, 0);
        
        
        rb.velocity = new Vector2(newV.x, rb.velocity.y);
        
        //currentAnimation = BasicMeleeAnimations.WALK;

    }
}
