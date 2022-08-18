using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Particle 
{
    [Header ("some properties")]
    public PARTICLENAME Name;
    public string Spin;
    public string Statistic;
    public string Family;
    public string Mass;
    public string MeanLifetime;
    public string Antiparticle;
    public string ElectrigCharge;
    public string ColorCharge = "none";
    public string BarionNumber = "none";
    public string LeptonNumber="none";

    [Header("InGame Fields")]
    public GameObject BeamPrefab;
    public GameObject EmittedParticlePrefab;
    public Sprite Icon;
}

public enum PARTICLENAME
{
    Electron, Positron, Photon
}
