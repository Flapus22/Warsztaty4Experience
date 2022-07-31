using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerPlacementData", menuName = "ScriptableObjects/TowerAttackData")]
public class TowerAttackData : ScriptableObject
{
    [field: SerializeField]
    public LayerMask EnemyLayerMask { get; set; }
    [field: SerializeField]
    public int AttackRadius { get; set; }
    [field: SerializeField]
    public int FireRate { get; set; }
    [field: SerializeField]
    public int Damage { get; set; }
    [field: SerializeField]
    public int ProjectilePrefab { get; set; }

}
