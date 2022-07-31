using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerPlacementData", menuName = "ScriptableObjects/TowerSelectionData")]

public class TowerSelectionData : ScriptableObject
{
    [field: SerializeField]
    public Color CanPlaceColor { get; set; }
    [field: SerializeField]
    public Color CanNotPlaceColor { get; set; }
    [field: SerializeField]
    public Color StartingColor { get; set; }
}
