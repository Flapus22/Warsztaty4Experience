using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [field: SerializeField]
    public int Health { get; set; }
    [field: SerializeField]
    public int Attack { get; set; }
    [field: SerializeField]
    public float Speed { get; set; }
    [field: SerializeField]
    public int CoinsDropped { get; set; }
}
