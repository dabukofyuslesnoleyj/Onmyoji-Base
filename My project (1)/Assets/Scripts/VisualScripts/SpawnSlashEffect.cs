using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSlashEffect : MonoBehaviour
{

    public GameObject slashEffectPrefab;
    // Start is called before the first frame update
    Vector2 slashDirection;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnEffect(Vector2 target)
    {
        slashDirection = (target - (Vector2)transform.position).normalized;

        Vector2 direction = slashDirection;
        GameObject newProjectile = Instantiate(slashEffectPrefab, target, Quaternion.identity);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        newProjectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
