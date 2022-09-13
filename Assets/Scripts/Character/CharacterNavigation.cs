using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterNavigation : MonoBehaviour
{
    [SerializeField] private CharacterNavigationSettings _settings;
    
    private float positionScaler = 3f;
    private float movementThreshold = 0.1f;
    
    private NavMeshAgent _navMeshAgent;
    public Vector3 Direction { get; set; }
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        SetSettings();
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        if (Direction.magnitude <= movementThreshold) return;
        var destination = transform.position + Direction * Time.deltaTime * positionScaler;
        _navMeshAgent.SetDestination(destination);
    }
    private void SetSettings()
    {
        positionScaler = _settings.PositionScaler;
        movementThreshold = _settings.MovementThreshold;
        _navMeshAgent.speed = _settings.NavigationSpeed;
    }
}
