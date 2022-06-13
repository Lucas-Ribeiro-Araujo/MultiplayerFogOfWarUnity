using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit")]
public class UnitScriptableObject : ScriptableObject
{
    [SerializeField]
    public int MaxHealth { get; private set; }
    [SerializeField]
    public int MoveSpeed { get; private set; }
    [SerializeField]
    public float AttackSpeed { get; private set; }
    [SerializeField]
    public float Range { get; private set; }
    [SerializeField]
    public float ProjectileSpeed { get; private set; }

}
