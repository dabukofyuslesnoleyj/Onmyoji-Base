using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class HitLagCaster : MonoBehaviour
{
    private Volume cameraVolume;
    private ChromaticAberration cA;
    private bool isWaiting;
    // Start is called before the first frame update
    void Start()
    { 
        GetPropertyComponents();
    }

    // Update is called once per frame
    void Update()
    {
        if(cameraVolume == null)
        {
            Debug.Log("Volume still null");
            GetPropertyComponents();
        }
        
    }

    public void CallHitLag(float duration)
    {
        if(isWaiting)
            return;
        
        Time.timeScale = 0.0f;
        StartCoroutine(DoHitLag(duration));
    }

    private void GetPropertyComponents()
    {
        cameraVolume = GameObject.FindGameObjectWithTag("GlobalPostProcessor").GetComponent<Volume>();
        if(cameraVolume != null)
        {
            if (cameraVolume.profile.TryGet(out ChromaticAberration chromaticAberration))
            {
                cA = chromaticAberration;
            }
            else
            {
                cA = null;
            } 
        }
         
    }

    IEnumerator DoHitLag(float duration)
    {
        isWaiting = true;
        // Get the volume component of the camera
        cameraVolume = Camera.main.GetComponent<Volume>();

        // Check if the volume component has a chromatic aberration override
        if (cA != null)
        {
            // Set the intensity of the chromatic aberration effect
            cA.intensity.value = 1.0f; // Replace 1.0f with your desired intensity value
        }

        yield return new WaitForSecondsRealtime(duration);

        cameraVolume = Camera.main.GetComponent<Volume>();
        if (cA != null)
        {
            // Set the intensity of the chromatic aberration effect
            cA.intensity.value = 0.0f; // Replace 1.0f with your desired intensity value
        }

        Time.timeScale = 1.0f;
        isWaiting = false;
    }
}
