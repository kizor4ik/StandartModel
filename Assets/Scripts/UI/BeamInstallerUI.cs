using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamInstallerUI : MonoBehaviour
{
    void Start()
    {
        BeamLauncher.BeamLaunchEvent += LaunchBeamAction;
    }

    private void LaunchBeamAction()
    {
        this.gameObject.GetComponent<UIEnabler>().Hide();
    }

}
