using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponImpact : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hitei");
    }
}
