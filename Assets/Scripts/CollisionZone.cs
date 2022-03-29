using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionZone : MonoBehaviour
{
    private bool _isCollided=false;

    // Start is called before the first frame update
    void Start()
    {
        //подписываемся на событие столкновения
        BeamLauncher.BeamCollideEvent += CollideBeam;
    }

    void CollideBeam()
    {
        _isCollided = true;
    }
      

    private void OnTriggerEnter(Collider collider)
    {
        if (_isCollided)
        {
            collider.transform.GetComponent<BeamMoving>().StopBeam();

        }
    }
}
