using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ScatteringDescription", menuName = "DataDescription/ScatteringDescription", order = 51)]
public class ScatteringDescription : ScriptableObject
{
    [SerializeField] private List<ScatteringProccess> _scatteringProcceses;
    public List<ScatteringProccess> ScatteringProcceses => _scatteringProcceses;
}
