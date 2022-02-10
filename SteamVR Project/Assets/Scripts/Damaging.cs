using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damaging : BaseObject
{
    public float damage = 1;
    public int uses = 1;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cube")
        {
            collision.gameObject.GetComponent<Cube>().Damage(damage);
            uses--;
            if (uses <= 0) Reset();
        }
    }
}
