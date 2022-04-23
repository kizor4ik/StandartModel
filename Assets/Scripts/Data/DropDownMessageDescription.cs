using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DropDownMessageDescription", menuName = "DataDescription/DropDownMessageDescription", order = 51)]
public class DropDownMessageDescription : ScriptableObject
{
    [SerializeField] private List<DropDownMessage> _message;
    public List<DropDownMessage> Message => _message;
}
