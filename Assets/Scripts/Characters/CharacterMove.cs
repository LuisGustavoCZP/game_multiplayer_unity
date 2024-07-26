using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMove : MonoBehaviour
{
    Animator animator;
    public NavMeshAgent agent;

    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public void GoTo (Vector3 position)
    {
        target = null;
        agent.SetDestination(position);
    }

    public void GoTo (GameObject go)
    {
        target = go;
        if(!target)
        {
            agent.SetDestination(transform.position);
            return;
        }
        agent.SetDestination(target.transform.position);
    }

    void Update ()
    {
        if(target) GoTo(target.transform.position);
        var velocity = transform.worldToLocalMatrix * agent.velocity;

        animator.SetFloat("right", velocity.x);
        animator.SetFloat("forward", velocity.z);
    }
}
