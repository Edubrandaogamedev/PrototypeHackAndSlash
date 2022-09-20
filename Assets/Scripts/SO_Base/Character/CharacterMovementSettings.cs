using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Game/Character/Movement")]
public class CharacterMovementSettings : ScriptableObject
{
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float controllerInputThreshold = 0.1f;
    [SerializeField] private bool usePhysics = false;
    public float Speed => movementSpeed;
    public float ControllerInputThreshold => controllerInputThreshold;
    public bool UsePhysics => usePhysics;

}
