using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour, IDashListener
{

    public static class OmnyojiAnimations
    {
        public static readonly string IDLE = "OmnyojiIdle";
        public static readonly string RUN = "OmnyojiRun";
        public static readonly string DASH = "OmnyojiDash";
        public static readonly string THROW = "OmnyojiThrow";
    }

    SpriteRenderer sprite;
    public Camera mainCamera;

    private Vector2 moveDirection;
    private Vector2 jumDirection;

    private GroundChecker groundChecker;

    private Rigidbody2D rb;
    private TrailRenderer tr;

    public ProjectileShooter shooter;
    private HitLagCaster hitLag;
    private Knockback knockback;
    private MaterialChanger materialChanger;

    private Dash dash;

    public float moveSpeed = 5.0f;
    public float jumpSpeed = 1.0f;

    public float throwDuration = 0.2f;

    public float markedAimRadius = 1f;

    private float slashDashRadius = 0.1f;
    public float hitLagDuration = 0.05f;

    private float throwTime;

    private bool _dashing;
    private bool _running;
    private bool _throwing;

    private bool _jumping;

    private SpawnSlashEffect slashEffect;
    private Dictionary<string, Transform> enemiesToSlash; 

    private string currentAnimation;
    private FrameByFrameAnimationController ac;

    private LayerMask enemyMask;

    private float horizontal;
    private float vertical;

    public bool isDashListening
    {
        get;
        set;
    }

    public bool isDashing 
    { 
        get => _dashing;
        set
        {
            if(_throwing && value)
                value = false;
            _dashing = value;
        }
    }

    public bool isRunning
    { 
        get => _running;
        set
        {
            if(_throwing && value)
                value = false;
            _running = value;
        }
    }
    public bool isThrowing
    { 
        get => _throwing;
        set
        {
            if(_dashing && _running && value)
                value = false;
            _throwing = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isDashListening = false;
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        dash = GetComponent<Dash>();
        tr = GetComponent<TrailRenderer>();
        slashEffect = GetComponent<SpawnSlashEffect>();
        hitLag = GetComponent<HitLagCaster>();

        groundChecker = GetComponent<GroundChecker>();
        groundChecker.boxCollider = GetComponent<BoxCollider2D>();
        groundChecker.extraHeightTest = 0.1f;

        ac = GetComponent<FrameByFrameAnimationController>();
        currentAnimation = OmnyojiAnimations.IDLE;

        knockback = GetComponent<Knockback>();
        materialChanger = GetComponent<MaterialChanger>();

        enemyMask = LayerMask.GetMask("Enemy");

        Physics2D.IgnoreLayerCollision(6, 7);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDashListening)
        {
            dash.AddListener(this);
        }
        FlipToMouse();

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        moveDirection = new Vector2(horizontal, 0);
        jumDirection = new Vector2(0, vertical);

        if(Input.GetMouseButtonDown(0))
        {
            Throw();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            DashToMarked();
            isDashing = true;

        }
        if(!isDashing)
        {
            tr.emitting = false;
        }

        if(Input.GetButtonDown("Jump"))
        {
            _jumping = true;
            
        }
    }

    private void FixedUpdate() {

        //Debug.Log(groundChecker.isGrounded());
        Run();
        
        Jump();

        if(isThrowing && Time.time > throwTime)
        {
            isThrowing = false;
        }
        isDashing = dash.GetIsDashing();
        if(!isRunning && !isThrowing && !isDashing)
        {
            currentAnimation = OmnyojiAnimations.IDLE;
        }
        if(isDashing)
        {
            currentAnimation = OmnyojiAnimations.DASH;
            //checkEnemyOnDash();
        }
        
        //Jump();
        ac.ChangeAnimation(currentAnimation);
    }

    private void Dash()
    {
        dash.CallDash(Input.mousePosition);
        tr.emitting = true;
    }

    private void DashToMarked()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] objectsInRadius = Physics2D.OverlapCircleAll(mousePos, markedAimRadius, enemyMask);

        if(objectsInRadius.Length > 0)
        {
            for(int i = 0; i < objectsInRadius.Length; i++)
            {
                Enemy e = objectsInRadius[i].gameObject.GetComponent<Enemy>();
                if(e.getIsMarked())
                {
                    //Debug.Log("Enemy Marked Dash");
                    dash.dashDuration = Vector2.Distance(transform.position, objectsInRadius[0].transform.position) / dash.dashSpeed;
                    dash.CallDash(objectsInRadius[0].transform.position);
                    enemiesToSlash.Add(e.EnemyID, e.transform);
                    e.Unmarked();
                    tr.emitting = true;
                    break;
                }

            }
            
        }

    }

    private void Run()
    {

        if(moveDirection.x != 0 && !isThrowing && !isDashing && groundChecker.isGrounded())
        {
            isRunning = true;
            rb.velocity = moveDirection * moveSpeed;
            currentAnimation = OmnyojiAnimations.RUN;
        }
        else
        {
            //if(groundChecker.isGrounded())
            //    rb.velocity = Vector2.zero;
            isRunning = false;
        }
    }

    private void Throw()
    {
        isThrowing = true;
        throwTime = Time.time + throwDuration;
        currentAnimation = OmnyojiAnimations.THROW;
        
        
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        
        shooter.Shoot(worldPosition);
    }

    private void Jump()
    {
        if(_jumping && groundChecker.isGrounded())
        {
            if(rb.velocity.x == 0)
                rb.velocity += new Vector2(horizontal, 1f).normalized * jumpSpeed;
            else
                rb.velocity += Vector2.up * jumpSpeed;
            //rb.AddForce((Vector2.up + moveDirection).normalized *700);
            //currentAnimation = "run";
        }
        _jumping = false;
    }

    private void FlipToMouse()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 objectPosition = mainCamera.WorldToScreenPoint(transform.position);
        Vector2 direction = mousePosition - objectPosition;
        sprite.flipX = direction.x > 0;

    }

    private void checkEnemyOnDash()
    {
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(transform.position, slashDashRadius, enemyMask);

        for(int i = 0; i < enemiesHit.Length; i++)
        {
            string id = enemiesHit[i].gameObject.GetComponent<Enemy>().EnemyID;
            if(enemiesToSlash != null)
            {
                if(!enemiesToSlash.ContainsKey(id))
                    enemiesToSlash.Add(id, enemiesHit[i].transform);
            }
            //slashEffect.SpawnEffect(enemiesHit[i].transform.position);
        }
    }

    public void OnStartDash()
    {
        enemiesToSlash = new Dictionary<string, Transform>();
    }
    public void OnFinishDash()
    {
        foreach(Transform t in enemiesToSlash.Values)
        {
            t.gameObject.GetComponent<Enemy>().Damaged(2);
            slashEffect.SpawnEffect(t.position);
        }
        if(enemiesToSlash.Values.Count > 0)
        {
            hitLag.CallHitLag(hitLagDuration);
        }
    }

    public void PlayerDamaged(float damage, Vector2 damageSourcePosition, float knockbackForce)
    {
        Vector2 direction = damageSourcePosition - new Vector2(transform.position.x, transform.position.y);
        knockback.ApplyKnockback(direction, knockbackForce);
        materialChanger.TemporaryChangeMaterial(0.3f);
    }

    public void DuringDash()
    {
        checkEnemyOnDash();
    }
}
