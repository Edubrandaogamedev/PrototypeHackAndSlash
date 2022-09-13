using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Game/Character/Navigation")]
public class CharacterNavigationSettings : ScriptableObject
{
    [Tooltip("This property increase the length of the direction Vector on destination calculation its another way to control the \"movement speed\"")]
    [SerializeField] private float positionScaler = 1f;
    [Tooltip("This property override the navmesh agent speed")]
    [SerializeField] private float navigationSpeed = 3.5f;
    [Tooltip("This property block the movement if the control not reach the threshold value")]
    [SerializeField] private float movementThreshold = 0.1f;

    public float PositionScaler => positionScaler;

    public float NavigationSpeed => navigationSpeed;

    public float MovementThreshold => movementThreshold;
}
