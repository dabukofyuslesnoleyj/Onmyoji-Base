using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCaster : MonoBehaviour
{
    public GameObject attackPrefab;
    public float shootingSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CastAttack(Vector2 target)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;;
        GameObject newProjectile = Instantiate(attackPrefab, transform.position, Quaternion.identity);
        if(direction.x < 0)
            newProjectile.GetComponent<SpriteRenderer>().flipX = false;
        newProjectile.GetComponent<Rigidbody2D>().velocity = direction * shootingSpeed;
    }
}
