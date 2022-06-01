using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEventsQualifier 
{
    private StatisticHandler _dataHandler;
    public GlobalEventsQualifier(StatisticHandler dataHandler)
    {
        _dataHandler = dataHandler;
    }
    // Мировое событие, отображаемое на TimeLine
    public delegate void GlobalEventTimeLineDelegate(GLOBAL_EVENT id);
    public static event GlobalEventTimeLineDelegate GlobalEventTimeLineEvent;


    // Для каждого GLOBAL_EVENT ниже, внутри описываются условия, при которых тригерится мировое событие
    public void CheckEvents(PARTICLENAME changedParticle)
    { 
        if (changedParticle == PARTICLENAME.Photon)
        {
            if (_dataHandler.Stats.RegisteredParticles[PARTICLENAME.Photon] == 1)
            {
                GlobalEventTimeLineEvent(GLOBAL_EVENT.TestPhotonDiscovery);
            }
        }

        if (changedParticle == PARTICLENAME.Electron)
        {
            if (_dataHandler.Stats.RegisteredParticles[PARTICLENAME.Electron] == 1)
            {
                GlobalEventTimeLineEvent(GLOBAL_EVENT.ElectronDiscovery);
            }
        }

        if (changedParticle == PARTICLENAME.Positron)
        {
            if (_dataHandler.Stats.RegisteredParticles[PARTICLENAME.Positron] == 1)
            {
                GlobalEventTimeLineEvent(GLOBAL_EVENT.PositronDiscovery);
            }
        }

        if (!_dataHandler.GlobalEventCompletion.IsEventCompleted[GLOBAL_EVENT.SpinDiscovery])
        {
            if (_dataHandler.Stats.RegisteredParticles[PARTICLENAME.Electron] + _dataHandler.Stats.RegisteredParticles[PARTICLENAME.Positron] > 10)
            {
                GlobalEventTimeLineEvent(GLOBAL_EVENT.SpinDiscovery);
            }
        }
    }
}
