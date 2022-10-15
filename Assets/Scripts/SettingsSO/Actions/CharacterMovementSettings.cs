using UnityEngine;
[CreateAssetMenu(menuName = "Game/Settings/Character/MovementActionSettings")]
public class CharacterMovementSettings : ActionSetting
{
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float controllerInputThreshold = 0.1f;
    [SerializeField] private bool usePhysics = false;
    public float Speed => movementSpeed;
    public float ControllerInputThreshold => controllerInputThreshold;
    public bool UsePhysics => usePhysics;

}
