using UnityEngine;

[CreateAssetMenu(fileName = "Agent Settings", menuName = "Game/Settings/Components/NavAgent")]
public class NavAgentSettings : Settings
{
    [SerializeField] private float speed = 3.5f;
    [SerializeField] private float angularSpeed = 120f;
    [SerializeField] private float acceleration = 8f;

    public float Speed => speed;
    public float AngularSpeed => angularSpeed;
    public float Acceleration => acceleration;
}