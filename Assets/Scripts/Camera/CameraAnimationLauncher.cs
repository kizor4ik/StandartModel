using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimationLauncher : MonoBehaviour
{
    [SerializeField] private Animator _cameraAnimator;
    void Start()
    {
        // подписываемся на событие, объвленное в другом классе
        BeamLauncher.BeamCollideEvent += BeamCollide;
    }

    private void Update()
    {

    }

    private void BeamCollide()
    {
        _cameraAnimator.SetTrigger("BeamCollision");
    }
}
