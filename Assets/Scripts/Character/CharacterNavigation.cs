using UnityEngine;
using UnityEngine.AI;

public class CharacterNavigation : MonoBehaviour
{
    [SerializeField] private CharacterMovementSettings _movementSettings;

    [SerializeField] private NavAgentSettingsOverride _agentSettings;

    private bool usePhysics;
    private float movementThreshold = 0.1f;
    
    private NavMeshAgent _navMeshAgent;
    private CharacterAnimation _animation;
    public Vector3 InputValue { get; set; }
    private float speed = 1f;
    
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animation = GetComponent<CharacterAnimation>();
        Setup();
    }
    private void Update()
    {
        if (InputValue.magnitude <= movementThreshold)
        {
            UpdateAnimation(0);
        }
        else
        {
            if (usePhysics)
            {
                _navMeshAgent.SetDestination(GetDestination());
            }
            else
            {
                transform.position = GetDestination();
                RotateTowardsDirection();
            }
            UpdateAnimation(InputValue.magnitude);
        }
    }
    private Vector3 GetDestination()
    {
        var destination = transform.position + InputValue * Time.deltaTime * speed;
        NavMeshHit hit;
        return NavMesh.SamplePosition(destination, out hit, .3f, NavMesh.AllAreas) ? hit.position : transform.position;
    }

    private void RotateTowardsDirection()
    {
        var angle = 90 - Mathf.Atan2(InputValue.z, InputValue.x) * Mathf.Rad2Deg;
        var euler = Quaternion.Euler(transform.localEulerAngles);
        var newRot = Quaternion.Euler(euler.x, angle, euler.z);
        transform.localRotation = newRot;
    }

    private void UpdateAnimation(float speed)
    {
        if (_animation == null) return;
        _animation.SetSpeed(speed);
    }
    private void Setup()
    {
        usePhysics = _movementSettings.UsePhysics;
        movementThreshold = _movementSettings.ControllerInputThreshold;
        speed = _movementSettings.Speed;
        _navMeshAgent.speed = _agentSettings.Speed;
        _navMeshAgent.angularSpeed = _agentSettings.AngularSpeed;
        _navMeshAgent.acceleration = _agentSettings.Acceleration;
    }
}
