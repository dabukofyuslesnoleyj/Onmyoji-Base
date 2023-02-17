using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigAttackCaster : MonoBehaviour
{
    public GameObject attackPrefab;
    public float shootingSpeed = 3f;
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
        Vector3 spawnPoint = transform.position - new Vector3(0f, 0.2f, 0f);
        GameObject newProjectile1 = Instantiate(attackPrefab, spawnPoint, Quaternion.identity);
        newProjectile1.GetComponent<Rigidbody2D>().velocity = Vector2.right * shootingSpeed;

        GameObject newProjectile2 = Instantiate(attackPrefab, spawnPoint, Quaternion.identity);
        newProjectile2.GetComponent<SpriteRenderer>().flipX = false;
        newProjectile2.GetComponent<Rigidbody2D>().velocity = Vector2.left * shootingSpeed;
    }
}
