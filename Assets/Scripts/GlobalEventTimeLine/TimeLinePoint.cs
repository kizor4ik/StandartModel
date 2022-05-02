using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLinePoint : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image _icon;
    [SerializeField] private UnityEngine.UI.Text _description;
    [SerializeField] private UnityEngine.UI.Text _name;
    [SerializeField] private UnityEngine.UI.Text _dateTime;

    private GlobalEvent _globalEvent; 

    public void SetEvent(GlobalEvent globalEvent)
    {
        _globalEvent = globalEvent;
        _icon.sprite = globalEvent.Icon;
        _description.text = globalEvent.Description;
        _name.text = globalEvent.ID;
        _dateTime.text = (globalEvent.DateTime / 10000).ToString();
    }

    public void Highlight()
    {
        _description.gameObject.SetActive(true);
    }

    public void UnHighlight()
    {
        _description.gameObject.SetActive(false);
    }
}
