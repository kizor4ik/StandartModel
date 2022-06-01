using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineUIEnabler : MonoBehaviour
{
    [SerializeField] TimeLineConstructor _timeLineConstructor;
    [SerializeField] FocusEvent _focusEvent;

    public void Show(GLOBAL_EVENT id)
    {
        this.gameObject.SetActive(true);
        _focusEvent.SetFocus(_timeLineConstructor.GlobalEventOnTimeLine[id]);
    }
    public void Show()
    {
        this.gameObject.SetActive(true);
    }
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
