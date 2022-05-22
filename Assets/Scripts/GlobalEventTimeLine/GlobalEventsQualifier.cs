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

    // Ниже описываются все мировые события для каждого GlobalEvent
    // Возможно, стоит все переписать и сделать только одно событие, которое будет выкидывать разный ID


    // Тестовое мировое событие с ID "TestPhotonDiscovery"
    public delegate void TestPhotonDiscoveryDelegate(GLOBAL_EVENT id);
    public static event TestPhotonDiscoveryDelegate TestPhotonDiscoveryEvent;

    // Тестовое мировое событие с ID "ElectronDiscovery"
    public delegate void ElectronDiscoveryDelegate(GLOBAL_EVENT id);
    public static event ElectronDiscoveryDelegate ElectronDiscoveryEvent;

    // Тестовое мировое событие с ID "PositronDiscovery"
    public delegate void PositronDiscoveryDelegate(GLOBAL_EVENT id);
    public static event PositronDiscoveryDelegate PositronDiscoveryEvent;

    public void CheckEvents()
    {
        if (_dataHandler.Stats.RegisteredParticles[PARTICLENAME.Photon] == 1)
        {
            TestPhotonDiscoveryEvent(GLOBAL_EVENT.TestPhotonDiscovery);
        }

        if (_dataHandler.Stats.RegisteredParticles[PARTICLENAME.Electron] == 1)
        {
            ElectronDiscoveryEvent(GLOBAL_EVENT.ElectronDiscovery);
        }

        if (_dataHandler.Stats.RegisteredParticles[PARTICLENAME.Positron] == 1)
        {
            PositronDiscoveryEvent(GLOBAL_EVENT.PositronDiscovery);
        }
    }
}
