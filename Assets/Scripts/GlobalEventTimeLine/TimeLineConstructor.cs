using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineConstructor : MonoBehaviour
{
    [SerializeField] private TimeLinePoint _template;
    [SerializeField] private GlobalEventsDescription _globalEventsDescription;
    [SerializeField] private float _placementStep;
    [SerializeField] private Vector3 _placementPosition=Vector3.zero;

  
    void Awake()
    {
        ConstructTimeLine();
    }

    public void ConstructTimeLine()
    {
        foreach (GlobalEvent globalEvent in _globalEventsDescription.ListOfGlobalEvents)
        {
            TimeLinePoint go = Instantiate(_template, this.transform);
            go.transform.localPosition = _placementPosition;
            _placementPosition = new Vector3(_placementPosition.x + _placementStep, _placementPosition.y, _placementPosition.z);
            go.SetEvent(globalEvent);
        }
        _template.gameObject.SetActive(false);
    }


}
