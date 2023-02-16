using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int markCounter;
    public int maxMarkCounter = 1;
    private bool isMarked;

    public GameObject deathEffect;

    public int hp = 3;

    public string EnemyID = "default";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate() {
        if(hp <= 0)
        {
            CallDeath();
        }
    }

    public void CallDeath()
    {
        GameObject death = Instantiate<GameObject>(deathEffect);
        death.transform.position = transform.position;
        Debug.Log(death.GetComponent<ParticleSystem>().isPlaying);
        Destroy(gameObject);
    }

    public void Marked()
    {
        markCounter++;
        
        if(markCounter >=maxMarkCounter)
            isMarked = true;
    }

    public void Unmarked()
    {
        markCounter = 0;
        isMarked = false;
    }

    public bool getIsMarked()
    {
        return isMarked;
    }

    public void Damaged(int damage)
    {
        gameObject.GetComponent<MaterialChanger>().TemporaryChangeMaterial(0.2f);
        hp-= damage;
    }
}
