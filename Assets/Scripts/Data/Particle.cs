using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Particle 
{
    [Header ("some properties")]
    public PARTICLENAME Name;
    public string Spin;

    [Header("InGame Fields")]
    public GameObject BeamPrefab;
    public GameObject EmittedParticlePrefab;
}

public enum PARTICLENAME
{
    Electron, Positron, Photon
}
