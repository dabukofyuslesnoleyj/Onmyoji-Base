using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEffectDisplayer : MonoBehaviour
{
    private ProjectileShooter parentShooter;
    private float displayDuration;
    private float displayTime;
    private bool isDisplaying;

    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        parentShooter = GetComponentInParent<ProjectileShooter>();
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        isDisplaying = false;
        displayDuration = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isDisplaying = true;
            displayTime = Time.time + displayDuration;
        }
        
    }

    private void FixedUpdate() {

        DisplaySprite();
        if(Time.time > displayTime)
        {
            isDisplaying = false;
        }
    }

    public void DisplaySprite()
    {
        sprite.enabled = isDisplaying;
    }
}
