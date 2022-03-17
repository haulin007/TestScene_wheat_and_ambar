using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Plant", menuName = "Garden", order = 1)]
public class PlantInfo : ScriptableObject
{
    [SerializeField]
    [Range(2, 20)]
    public int _wheatHealt;
    [SerializeField]
    [Range(1, 50)]
    public int _cost; 
}
