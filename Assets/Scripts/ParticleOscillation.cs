using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOscillation : MonoBehaviour
{
    private float _moveValueX;
    private float _moveValueY;
    private float _moveValueZ;
    [SerializeField] private float _angularVelocityX;
    [SerializeField] private float _angularVelocityY;
    [SerializeField] private float _angularVelocityZ;
    [SerializeField] private float _radius;
    [SerializeField] private Vector3 _center;
    private bool _isLaunched = true;

    private void Start()
    {
        //подписываемся на событие столкновения
        BeamLauncher.BeamCollideEvent += StopOscillation;
    }

    void Update()
    {
        if (_isLaunched)
        {
            _moveValueX += Time.deltaTime * _angularVelocityX;
            _moveValueY += Time.deltaTime * _angularVelocityY;
            _moveValueZ += Time.deltaTime * _angularVelocityZ;

            float x = _radius * Mathf.Sin(_moveValueX);
            float y = _radius * Mathf.Cos(_moveValueY);
            float z = _radius * Mathf.Cos(_moveValueZ);

            transform.localPosition = _center + new Vector3(x, y, z);
        }
    }

    private void StopOscillation()
    {
        _isLaunched = false;
    }
}
