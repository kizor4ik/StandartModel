using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmittedParticle : MonoBehaviour
{
    [SerializeField] private PARTICLENAME _name;
    [SerializeField] private float _radius;
    [SerializeField] private float _initialSpeed;
    [SerializeField] private float _lifetime;
    private float _speed;
    private Vector3 _direction;    
    private float _time=0;
    private bool _isEmmited = false;

    //объявляем событие подбора частицы
    public delegate void GrabParticleDelegate(PARTICLENAME name);
    public static event GrabParticleDelegate GrabParticleEvent;

    private void Start()
    {
        //подписываемся на событие столкновения
        BeamLauncher.BeamCollideEvent += Emmit;
    }

    void Update()
    {
        if (_isEmmited)
        {
            ExponentialParticleSlowDown();
            transform.localPosition += _direction * _speed * Time.deltaTime;
        }

        if (_time > _lifetime)
        {
            DeactivateParticle();
        }
    }

    private void DeactivateParticle()
    {
        this.gameObject.SetActive(false);
    }

    private void Emmit()
    {
        this.GetComponent<MeshRenderer>().enabled = true;

        _isEmmited = true;
        float angle = Random.Range(0, 2 * Mathf.PI);
        _direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
    }

    private void ExponentialParticleSlowDown()
    {
        _time += Time.deltaTime;
        _speed = _initialSpeed * Mathf.Exp(-_time * _initialSpeed / _radius);
    }

    public void GrabEmittedParticle()
    {
        GrabParticleEvent(_name);
        this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        BeamLauncher.BeamCollideEvent -= Emmit;
    }
}
