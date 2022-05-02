using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusEvent : MonoBehaviour
{
    private TimeLinePoint _previousTimeLinePoint;

    public void SetFocus(TimeLinePoint timeLinePoint)
    {
        if (_previousTimeLinePoint != null)
        {
            _previousTimeLinePoint.UnHighlight();
        }
        timeLinePoint.Highlight();
        _previousTimeLinePoint = timeLinePoint;
    }
}
