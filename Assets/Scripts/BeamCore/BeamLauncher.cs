using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamLauncher : MonoBehaviour
{
   // [SerializeField] private BeamMoving _beamMoving;

    //объявляем событие запуска пучка
    public delegate void BeamLaunchDelegate();
    public static event BeamLaunchDelegate BeamLaunchEvent;

    //объявляем событие столкновения пучка
    public delegate void BeamCollideDelegate();
    public static event BeamCollideDelegate BeamCollideEvent;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            //событие запуска пучка
            BeamLaunchEvent();
        }
   
        if (Input.GetKeyUp(KeyCode.S))
        {
            //событие столкновения пучка
            BeamCollideEvent();
        }
    }
  
}
