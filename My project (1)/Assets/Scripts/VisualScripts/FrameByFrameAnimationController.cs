using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameByFrameAnimationController : MonoBehaviour
{

    private Animator animator;

    private string currentAnimation;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeAnimation(string nextAnimation)
    {
        if(currentAnimation == nextAnimation)
            return;

        currentAnimation = nextAnimation;

        animator.Play(nextAnimation);
    }
}
