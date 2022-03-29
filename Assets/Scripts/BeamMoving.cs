using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamMoving : MonoBehaviour
{
    
    private float _moveValue;
    [SerializeField] private float _angularVelocity;
    [SerializeField] private float _radius;
    [SerializeField] private Vector3 _center;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _accelerationDamping;
    public bool _isLaunched = false;
    private bool _isCollided = false;

    private float _timeSpent = 0;

    private void Start()
    {
        _moveValue = Mathf.Atan(transform.localPosition.x / transform.localPosition.y);
        // подписываемся на события, объвленное в другом классе
        //подписываемя на событие запуска
        BeamLauncher.BeamLaunchEvent += LaunchBeam;
        //подписываемся на событие столкновения
        BeamLauncher.BeamCollideEvent += CollideBeam;
    }
    void Update()
    {
        if (_isLaunched)
        {
            AccelerateBeam();
            _moveValue += Time.deltaTime * _angularVelocity;
            float x = _radius * Mathf.Sin(_moveValue);
            float y = _radius * Mathf.Cos(_moveValue);
            transform.localPosition = _center + new Vector3(x, y, 0);
        }
    }

    private void LaunchBeam()
    {
        if(!_isCollided)
            _isLaunched = true;
    }

    private void CollideBeam()
    {
        _isCollided = true;
    }

    public void StopBeam()
    {
        _isLaunched = false;
    }

    private void AccelerateBeam()
    {
            if (!_isCollided && _acceleration*_accelerationDamping > 0 )
            {
                _timeSpent += Time.deltaTime;
                _angularVelocity += Time.deltaTime * _acceleration;
                _acceleration -= Time.deltaTime * _accelerationDamping;
            }
    }

}
