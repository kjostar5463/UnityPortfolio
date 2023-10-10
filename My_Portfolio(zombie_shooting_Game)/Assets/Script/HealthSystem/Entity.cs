using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    protected float health;

    public void TakeHit(float damage, GameObject clone)
    {
        clone.GetComponent<Entity>().health -= damage;
    }
}