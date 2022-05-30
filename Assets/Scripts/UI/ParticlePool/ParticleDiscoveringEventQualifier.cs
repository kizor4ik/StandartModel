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
    public delegate void ParticlePoolRefreshDelegate(PARTICLENAME id);
    public static event ParticlePoolRefreshDelegate ParticlePoolRefreshEvent;


    public void CheckEvents(GLOBAL_EVENT changedEvent)
    {
        if (changedEvent == GLOBAL_EVENT.TestPhotonDiscovery)
        {
            //ParticlePoolRefreshEvent(UIREFRESH_EVENT.TestPhotonDiscovery);  
            ParticlePoolRefreshEvent(PARTICLENAME.Photon);
        }
    }
}
