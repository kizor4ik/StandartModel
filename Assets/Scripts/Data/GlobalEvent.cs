using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GlobalEvent 
{
    public GLOBAL_EVENT Name;
    public Sprite Icon;
    [Header("Date in format yyyymmdd")]
    public int DateTime;
    public string Description;
    public DropDownMessage Message;
}

public enum GLOBAL_EVENT
{
    ElectronDiscovery, PositronDiscovery, TestPhotonDiscovery, SpinDiscovery, ElectronNeutrinoDiscovery, AntiElectronNeutrinoDiscovery,
    WeinbergGlashowSalamModel
}