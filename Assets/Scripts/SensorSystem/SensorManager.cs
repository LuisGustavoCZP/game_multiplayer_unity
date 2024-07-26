using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SensorManager : MonoBehaviour
{
    public LayerMask layers;
    public List<SensorTarget> targets = new List<SensorTarget>();
    public List<CharacterSensor> target = new List<CharacterSensor>();

    void LateUpdate()
    {
        CheckVision();
    }

    void CheckVision ()
    {
        targets.Clear();
        var thisTransform = transform;
        Collider[] colliders = new Collider[20];
        var colisionsCount = Physics.OverlapSphereNonAlloc(transform.position, 100, colliders, layers);
        
        targets.Capacity = colisionsCount;
        foreach (var otherCollider in colliders)
        {
            if(!otherCollider) break;
            if(otherCollider.gameObject == gameObject) continue;

            var otherName = otherCollider.gameObject.name;
            var tr = otherCollider.transform;
            var distanceVector = tr.position - thisTransform.position;
            var distance = Vector3.Magnitude(distanceVector);
            var direction = Vector3.Normalize(distanceVector);
            var colliderAngle = Vector3.SignedAngle(thisTransform.forward, direction, Vector3.up);

            var rb = otherCollider.attachedRigidbody;
            var ag = otherCollider.GetComponent<NavMeshAgent>();
            var agVelocity = ag ? ag.velocity : Vector3.zero;
            var rgVelocity = rb? rb.velocity : Vector3.zero;
            float areaCost = 0; 

            if(ag && ag.Raycast(tr.position, out NavMeshHit area)) areaCost = ag.GetAreaCost(area.mask);

            var target = new SensorTarget(){
                name = otherName,
                velocity = agVelocity != Vector3.zero ? agVelocity : rgVelocity,
                position = tr.position,
                direction = direction,
                forward = tr.forward,
                acceleration = rb? rb.GetAccumulatedForce() : Vector3.zero,
                areaCost = areaCost,
                type = otherCollider.tag,
            };

            targets.Add(target);
        }
    }
}