using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkDisplayer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    private Enemy parentEnemy;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        parentEnemy = GetComponentInParent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        var isMarked = parentEnemy.getIsMarked();

        if(spriteRenderer != null){
            spriteRenderer.enabled = isMarked;
        }
    }
}
