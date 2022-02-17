using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionOnCollision : MonoBehaviour
{
    public UnityEvent onTriggerOrCollision;

    private void OnCollisionEnter(Collision collision)
    {
        onTriggerOrCollision.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        onTriggerOrCollision.Invoke();
    }
}
