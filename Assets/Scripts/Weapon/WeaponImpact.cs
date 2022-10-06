using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class WeaponImpact : MonoBehaviour
{
    private Collider _collider;
    public event Action<ITakeDamage> OnHit = delegate{};
    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void EnableCollider()
    {
        _collider.enabled = true;
    }
    public void DisableCollider()
    {
        _collider.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        var damageableEntity = other.GetComponent<ITakeDamage>();
        if (damageableEntity != null)
        {
            OnHit(damageableEntity);
        }
    }
}
