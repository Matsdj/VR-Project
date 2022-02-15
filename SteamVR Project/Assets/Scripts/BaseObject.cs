using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseObject : MonoBehaviour
{
    public float minimumAltitude = -1;
    public float gravity = 9.81f;
    private Vector3 startScale;

    public UnityEvent<BaseObject> ResetEvent = new UnityEvent<BaseObject>();
    Rigidbody rb;
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        startScale = transform.localScale;
        rb.useGravity = false;
    }

    public virtual void Update()
    {
        if (transform.position.y < minimumAltitude) Reset();
    }

    public virtual void FixedUpdate()
    {
        rb.AddForce(Vector3.down * gravity * Time.fixedDeltaTime * 10);
    }

    public virtual void Reset()
    {
        gameObject.SetActive(false);

        transform.localScale = startScale;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        ResetEvent.Invoke(this);
    }
}
