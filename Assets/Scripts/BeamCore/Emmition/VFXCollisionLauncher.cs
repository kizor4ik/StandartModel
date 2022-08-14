using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXCollisionLauncher : MonoBehaviour
{
    [SerializeField] private bool _isActive = false;
    void Start()
    {
        if(_isActive)
        {
            BeamLauncher.BeamCollideEvent += ActivateEffect;
            gameObject.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        BeamLauncher.BeamCollideEvent -= ActivateEffect;
    }


    private void ActivateEffect()
    {
        Debug.Log("VFX happens");
        gameObject.SetActive(true);
    }
}
