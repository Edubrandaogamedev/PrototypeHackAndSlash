using UnityEngine;
[CreateAssetMenu(menuName = "Game/Settings/Components/NavMeshAgentOverride")]
public class NavAgentSettingsOverride : ScriptableObject
{
    [SerializeField] private float speed = 3.5f;
    [SerializeField] private float angularSpeed = 120f;
    [SerializeField] private float acceleration = 8f;

    public float Speed => speed;
    public float AngularSpeed => angularSpeed;
    public float Acceleration => acceleration;
}