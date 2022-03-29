using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ScatteringProccess
{
    [Header("some properties")]
    public Reagents Reagents;
    public List<Channel> Channels;
}

[System.Serializable]
public class Channel
{
    public float ProbabilityOfChannel;
    public List<PARTICLENAME> RadiatedParticles; 
}

[System.Serializable]
public class Reagents
{
    public PARTICLENAME BeamOne;
    public PARTICLENAME BeamTwo;

    
    public Reagents(PARTICLENAME beamOne, PARTICLENAME beamTwo)
    {
        BeamOne = beamOne;
        BeamTwo = beamTwo;

    }
}


