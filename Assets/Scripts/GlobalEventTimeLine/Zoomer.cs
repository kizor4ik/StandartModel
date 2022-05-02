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

    private void Start()
    {
        _initialScale = transform.localScale;
    }
    private void Update()
    {
        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }
    private void Zoom(float increment)
    {
        _scaleFactor +=  increment* _sensitivity;
        _scaleFactor = Mathf.Clamp(_scaleFactor, _minScaleFactor, _maxScaleFactor);
        transform.localScale = _initialScale * _scaleFactor;
    }
}
