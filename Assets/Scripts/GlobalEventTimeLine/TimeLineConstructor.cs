using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineConstructor : MonoBehaviour
{
    [SerializeField] private StatisticHandler _statisticHandler;
    [SerializeField] private TimeLinePoint _template;
    [SerializeField] private GlobalEventsDescription _globalEventsDescription;
    [SerializeField] private float _placementStep;
    [SerializeField] private Vector3 _placementPosition = Vector3.zero;

    private Dictionary<GLOBAL_EVENT, TimeLinePoint> _globalEventOnTimeLine = new Dictionary<GLOBAL_EVENT, TimeLinePoint>();
    public Dictionary<GLOBAL_EVENT, TimeLinePoint> GlobalEventOnTimeLine => _globalEventOnTimeLine;
    private GlobalEventSubscriber _globalEventSubscriber;

    void Start()
    {
        _globalEventSubscriber = new GlobalEventSubscriber(this, _statisticHandler);
        ConstructTimeLine();
    }

    public void ConstructTimeLine()
    {
        foreach (GlobalEvent globalEvent in _globalEventsDescription.ListOfGlobalEvents)
        {
            TimeLinePoint go = Instantiate(_template, this.transform);
            _globalEventOnTimeLine.Add(globalEvent.Name, go);
            go.transform.localPosition = _placementPosition;
            _placementPosition = new Vector3(_placementPosition.x + _placementStep, _placementPosition.y, _placementPosition.z);
            go.SetEvent(globalEvent);
            if (!_statisticHandler.GlobalEventCompletion.IsEventCompleted[globalEvent.Name])
            {
                go.transform.gameObject.SetActive(false);
            }
        }
        _template.gameObject.SetActive(false);
    }

    public void UpdateTimeLine(GLOBAL_EVENT id)
    {
        _globalEventOnTimeLine[id].transform.gameObject.SetActive(true);
    }
}
