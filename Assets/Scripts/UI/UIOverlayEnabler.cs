using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOverlayEnabler : MonoBehaviour
{
    private void Awake()
    {
        BeamLauncher.BeamLaunchEvent += Hide;
        BeamLauncher.BeamCollideEvent += DelayedShow;
    }
    private void DelayedShow()
    {
        Invoke("Show", 1f);
    }
    public void Show()
    {
        this.gameObject.SetActive(true);
    }
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        BeamLauncher.BeamLaunchEvent -= Hide;
        BeamLauncher.BeamCollideEvent -= DelayedShow;
    }
}
