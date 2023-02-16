using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitLagCaster : MonoBehaviour
{

    private bool isWaiting;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallHitLag(float duration)
    {
        if(isWaiting)
            return;
        
        Time.timeScale = 0.0f;
        StartCoroutine(DoHitLag(duration));
    }

    IEnumerator DoHitLag(float duration)
    {
        isWaiting = true;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = 1.0f;
        isWaiting = false;
    }
}
