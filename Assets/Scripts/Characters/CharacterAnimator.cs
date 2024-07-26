using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public List<string> animationPairs = new();
    Dictionary<string, int> animations = new();
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        for (int i = 0; i < animationPairs.Count; i++)
        {
            animations.Add(animationPairs[i], i);
        }
    }

    public void SetAnimation(string name)
    {
       animator.SetInteger("state", animations[name]);
    }
}
