using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealCollision : MonoBehaviour
{
    public LayerMask enemyLayer;
    private float attackRadius = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        enemyLayer = LayerMask.GetMask("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRadius, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().Marked();
            Destroy(gameObject);
        }
    }
}
