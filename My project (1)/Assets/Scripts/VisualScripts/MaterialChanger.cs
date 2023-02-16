using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{

    private Material primaryMaterial;
    private Material currentMaterial;
    public Material secondaryMaterial;

    private SpriteRenderer sprite;

    private float changeTime;


    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        primaryMaterial = sprite.material;
        currentMaterial = primaryMaterial;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(currentMaterial != primaryMaterial && Time.time > changeTime)
        {
            currentMaterial = primaryMaterial;
            sprite.material = currentMaterial;
        }
    }

    public void TemporaryChangeMaterial(float duration)
    {
        if(secondaryMaterial != null)
        {
            if(currentMaterial == primaryMaterial)
            {
                changeTime = Time.time + duration;
                currentMaterial = secondaryMaterial;
                sprite.material = currentMaterial;
            }
        }
    }
}
