using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Zoomer : MonoBehaviour
{
    [SerializeField] private float _minScaleFactor = 0.1f;
    [SerializeField] private float _maxScaleFactor = 2f;
    [SerializeField] private float _sensitivity = 0.1f;

    private float _scaleFactor = 1;
    private Vector3 _initialScale;

    private Vector3 _scaleVelocity = Vector3.zero;
    private float _scaleSmoothTime = 0.3f;

    private void Start()
    {
        _initialScale = transform.localScale;
    }
    private void Update()
    {
        float increment = Input.GetAxis("Mouse ScrollWheel");
        if (increment != 0)
        {
            Zoom(increment);
        }
        
    }
    private void Zoom(float increment)
    {
        _scaleFactor +=  increment* _sensitivity;
        _scaleFactor = Mathf.Clamp(_scaleFactor, _minScaleFactor, _maxScaleFactor);
        transform.localScale = _initialScale * _scaleFactor;
    }

    public void FocusZoom()
    {
        transform.localScale = Vector3.SmoothDamp(transform.localScale, _initialScale, ref _scaleVelocity, _scaleSmoothTime);
        _scaleFactor = Vector3.SqrMagnitude(transform.localScale)/Vector3.SqrMagnitude(_initialScale);
    }
    
}
