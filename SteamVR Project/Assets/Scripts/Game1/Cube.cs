using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : BaseObject
{
    public float health = 1;
    public float maxHealth = 1;
    public int points = 1;
    public bool scalesFromDamage;

    public void Damage(float amount)
    {
        if (health > 0)
        {
            health -= amount;
            if (scalesFromDamage) transform.localScale = Vector3.one * health / maxHealth;
            if (health <= 0)
            {
                Game1.instance.Points += points;
                Reset();
            }
        }
        
    }

    public override void Reset()
    {
        base.Reset();
        health = maxHealth;
    }
}
