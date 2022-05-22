using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusEvent : MonoBehaviour
{
    [SerializeField] private Zoomer _zoomer;
    [SerializeField] private float _smoothTime = 0.3f;
    private TimeLinePoint _previousTimeLinePoint;
    private Coroutine _runningCoroutine;

    public void SetFocus(TimeLinePoint timeLinePoint)
    { 
            if (_previousTimeLinePoint != null)
            {
                _previousTimeLinePoint.UnHighlight();
            }
            timeLinePoint.Highlight();
            _previousTimeLinePoint = timeLinePoint;

            if (_runningCoroutine != null)
            {
                StopCoroutine(_runningCoroutine);
            }
            _runningCoroutine = StartCoroutine(MyCoroutine(timeLinePoint.transform));
    }

    IEnumerator MyCoroutine(Transform target)
    {
        Vector3 velocity = Vector3.zero;
        Vector3 targetPosition = -target.transform.localPosition;
        while (Vector3.Magnitude(transform.localPosition - targetPosition) > 2f)
        {
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPosition, ref velocity, _smoothTime);
            _zoomer.FocusZoom();
            yield return null;
        }
    }
}
