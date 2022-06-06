using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDiscoveringEventQualifier
{
    private StatisticHandler _dataHandler;
    public ParticleDiscoveringEventQualifier (StatisticHandler dataHandler)
    {
        _dataHandler = dataHandler;
    }
    // Событие, изменяющие UIParticlePool
    public delegate void ParticleDiscoveryDelegate(PARTICLENAME id);
    public static event ParticleDiscoveryDelegate ParticleDiscoveryEvent;


    public void CheckEvents(GLOBAL_EVENT changedEvent)
    {
        if (changedEvent == GLOBAL_EVENT.TestPhotonDiscovery)
        { 
            ParticleDiscoveryEvent(PARTICLENAME.Photon);
        }
    }
}
 