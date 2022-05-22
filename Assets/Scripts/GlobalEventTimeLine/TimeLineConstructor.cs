using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

public class TimeLineConstructor : MonoBehaviour
{
    [SerializeField] private TimeLinePoint _template;
    [SerializeField] private GlobalEventsDescription _globalEventsDescription;
    [SerializeField] private float _placementStep;
    [SerializeField] private Vector3 _placementPosition = Vector3.zero;

    private Dictionary<GLOBAL_EVENT, TimeLinePoint> _globalEventOnTimeLine = new Dictionary<GLOBAL_EVENT, TimeLinePoint>();
    private GlobalEventCompletion _globalEventCompletion = new GlobalEventCompletion();
    private GlobalEventSubscriber _globalEventSubscriber;

    void Awake()
    {
       // BayatGames.SaveGameFree.SaveGame.Clear();
        ConstructTimeLine();
        _globalEventSubscriber = new GlobalEventSubscriber(this);
    }

    public void ConstructTimeLine()
    {
        Load();
        foreach (GlobalEvent globalEvent in _globalEventsDescription.ListOfGlobalEvents)
        {
            TimeLinePoint go = Instantiate(_template, this.transform);
            _globalEventOnTimeLine.Add(globalEvent.Name, go);
            go.transform.localPosition = _placementPosition;
            _placementPosition = new Vector3(_placementPosition.x + _placementStep, _placementPosition.y, _placementPosition.z);
            go.SetEvent(globalEvent);
            if (!_globalEventCompletion.IsEventCompleted[globalEvent.Name])
            {
                go.transform.gameObject.SetActive(false);
            }
        }
        _template.gameObject.SetActive(false);
    }

    public void UpdateTimeLine(GLOBAL_EVENT id)
    {  
        _globalEventCompletion.IsEventCompleted[id] = true;
        _globalEventOnTimeLine[id].transform.gameObject.SetActive(true);
        Save();
    }

    private void Save()
    {
        SaveGame.Save<GlobalEventCompletion>(SAVE_LOAD_KEYS.GlobalEventCompletion.ToString(), _globalEventCompletion);
    }

    private void Load()
    {
        if (SaveGame.Exists(SAVE_LOAD_KEYS.GlobalEventCompletion.ToString()))
        {
            _globalEventCompletion = SaveGame.Load<GlobalEventCompletion>(SAVE_LOAD_KEYS.GlobalEventCompletion.ToString(), new GlobalEventCompletion());
        }
        else
        {
            foreach (GlobalEvent globalEvent in _globalEventsDescription.ListOfGlobalEvents)
            {
                _globalEventCompletion.IsEventCompleted.Add(globalEvent.Name, false);
            }
        }
    }


}
