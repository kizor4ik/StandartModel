using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GlobalEventsDescription", menuName = "DataDescription/GlobalEventsDescription", order = 51)]
public class GlobalEventsDescription : ScriptableObject
{
    [SerializeField] private List<GlobalEvent> _listOfGlobalEvents;
    public List<GlobalEvent> ListOfGlobalEvents => _listOfGlobalEvents;
}


