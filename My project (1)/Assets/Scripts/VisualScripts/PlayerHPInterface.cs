using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPInterface : MonoBehaviour
{
    TextMesh textMesh;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerHPChanged(int value)
    {
        
    }
}
