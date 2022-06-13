using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitController : MonoBehaviour
{

    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }
    public int MoveSpeed { get; private set; }
    public int MoveSpeedModifier { get; private set; }
    public float AttackSpeed { get; private set; }
    public float AttackSpeedModifier { get; private set; }
    public float Range { get; private set; }
    public float ProjectileSpeed { get; private set; }

    [SerializeField] private UnitScriptableObject baseStatsSO;

    private NavMeshAgent NavigationAgent;

    public ITargetable target { get; private set; }

    private void Awake()
    {
        NavigationAgent = this.GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        MaxHealth = baseStatsSO.MaxHealth;
        MoveSpeed = baseStatsSO.MoveSpeed;
        AttackSpeed = baseStatsSO.AttackSpeed;
        Range = baseStatsSO.Range;
        ProjectileSpeed = baseStatsSO.ProjectileSpeed;

        NavigationAgent.speed = MoveSpeed;

        CurrentHealth = MaxHealth;
    }

    void Update()
    {

    }

    internal void SetTarget(Vector3 point)
    {
        NavigationAgent.SetDestination(point);
    }
}
