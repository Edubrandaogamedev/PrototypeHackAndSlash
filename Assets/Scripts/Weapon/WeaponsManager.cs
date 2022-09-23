using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponsManager : MonoBehaviour
{
    [SerializeField] private Transform mainHandPosition;
    [SerializeField] private Weapon currentWeapon;
    private void Update()
    {
        currentWeapon.SetWeaponOnCorrectPosition(mainHandPosition.position,mainHandPosition.rotation);
    }
}
