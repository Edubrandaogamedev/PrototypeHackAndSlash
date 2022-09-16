using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Game/Character/Movement")]
public class CharacterNavigationSettings : ScriptableObject
{
    [SerializeField] private float movementThreshold = 0.1f;
    [SerializeField] private bool usePhysics = false;
    public float MovementThreshold => movementThreshold;
    public bool UsePhysics => usePhysics;

}
